import React, { useState, useEffect, useCallback } from "react";
import Button from 'react-bootstrap/Button';
//import Modal from 'react-bootstrap/Modal';
import Styles from "./AddStockButton.css";
import { ModalConfirm, ModalError } from "../../../../components/Common/Modals/Modals.jsx";
import { AlertDanger } from "../../../../components/Common/Alerts/Alerts.jsx";
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { Typeahead } from 'react-bootstrap-typeahead';
import { OnlyInteger } from "../../../../Utils/InputText.js";





const AddStockButton = ({ refreshTablePortfolio }) => {
    //Modal
    const [showModal, setShowModal] = useState(false);
    //Listado de acciones para el autocomplete
    const [stocks, setStocks] = useState([]);
    //Valores seleccionados
    const [stockSelected, setStockSelected] = useState([]);
    const [stockQuatity, setStockQuantity] = useState("");
    const [stockPrice, setStockPrice] = useState("");
    //Errores
    const [showErrorContainer, setShowErrorContainer] = useState(false);
    const [errorContent, setErrorContent] = useState([]);
    //Modal de Error
    const [showErrorModal, setShowErrorModal] = useState(false);
    const [ErrorMessage, setErrorMessage] = useState("");




    const loadStocksAutocomplete = useCallback(() => {
        fetch("api/Stocks/GetStockAutocomplete")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStocks(responseJson);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        if (showModal) {
            //reiniciamos los valores del modal
            setStockSelected([]);
            setStockQuantity("");
            setStockPrice("");
            //Contenedor de error del modal
            setErrorContent([]);
            setShowErrorContainer(false);
        }
        //Combo de autocomplete
        loadStocksAutocomplete();
    }, [showModal, loadStocksAutocomplete]);



    //Evento para input de cantidad
    const handleQuantityInput = (event) => {
        const value = event.target.value;
        //Solo permite numeros enteros
        if (OnlyInteger(value)) {
            setStockQuantity(value);
        }
    }

    //Evento para input de precio promedio de compra
    const handlePriceInput = (event) => {
        setStockPrice(event.target.value);
    }


    //Abre el modal de error
    function openErrorModal(message) {
        setErrorMessage(message);
        setShowErrorModal(true);
    }


    //Enviamos la nueva accion del portfolio
    function SendNewStockPortfolio() {
        let errors = [];

        //Ocultamos el container de errors
        setShowErrorContainer(false);
        //Validamos los datos de la nueva accion del portfolio
        if (stockSelected[0] == undefined) {
            errors.push("Debe seleccionar una compañia");
        }
        //Validamos la cantidad de acciones
        if ((stockQuatity == "") || (stockQuatity == 0)) {
            errors.push("La cantidad de acciones debe ser mayor que 0");
        }
        //Validamos el precio de la accion
        if ((stockPrice == "") || (isNaN(stockPrice))) {
            errors.push("El precio de compra no es correcto");
        }



        //Guardamos los errores
        setErrorContent(errors);

        //Validamos si hay errores
        if (errors.length == 0) {
            //datos de la nueva accion
            const requestOptions = {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    'quoteId': stockSelected[0].id,
                    'quantity': parseInt(stockQuatity),
                    'price': parseFloat(stockPrice)
                })
            };

            fetch("api/Portfolio/AddPortfolioStock", requestOptions)
                .then(async response => {
                    switch (response.status) {
                        case 200: //OK
                            setShowModal(false);
                            refreshTablePortfolio();
                            break;
                        case 400: //BADREQUEST
                            let textError400 = await response.text();
                            let jsonErrors400 = JSON.parse(textError400).errors
                            setErrorContent(Object.values(jsonErrors400));
                            setShowErrorContainer(true);
                            break;
                        default:
                            let textError = await response.text();
                            throw new Error(textError);
                            break;
                    }
                })
                .catch(error => {
                    //Modal de ERROR
                    openErrorModal(error.toString());
                })



        } else {
            //Hay errores
            setShowErrorContainer(true);
        }





    }




    const newPortfolioStockContent = () => {

        return (
            <>
                <Form>
                    <Form.Group as={Row} className="mb-3" controlId="formHorizontalStock">
                        <Form.Label column sm={2}>
                            Stock
                        </Form.Label>
                        <Col sm={10}>
                            <Typeahead
                                id="basic-typeahead-single"
                                labelKey="displayName"
                                onChange={setStockSelected}
                                options={stocks}
                                placeholder="Símbolo o nombre de la compañia..."
                                selected={stockSelected}
                            />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3" controlId="formHorizontalQuantity">
                        <Form.Label column sm={2}>
                            Cantidad
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control
                                type="text"
                                value={stockQuatity}
                                onChange={handleQuantityInput}
                            />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} className="mb-3" controlId="formHorizontalPrice">
                        <Form.Label column sm={2}>
                            Precio
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control
                                type="number"
                                value={stockPrice}
                                onChange={handlePriceInput}
                            />
                        </Col>
                    </Form.Group>

                </Form>
                <AlertDanger show={showErrorContainer} content={errorContent} />
            </>

        )
    }


    return (
        <>
            <Button variant="success" onClick={() => setShowModal(true)}>Agregar nueva acción</Button>
            <ModalConfirm
                show={showModal}
                content={newPortfolioStockContent()}
                buttonNoTitle="Cancelar"
                onButtonNoClick={() => setShowModal(false)}
                buttonYesTitle="Agregar"
                onButtonYesClick={SendNewStockPortfolio}
            />
            <ModalError
                show={showErrorModal}
                content={ErrorMessage}
                onClose={() => setShowErrorModal(false)}
            />

        </>
    )
}

export default AddStockButton;