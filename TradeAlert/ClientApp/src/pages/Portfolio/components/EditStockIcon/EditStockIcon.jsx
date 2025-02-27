import React, { useState, useEffect } from "react";
import Styles from "./EditStockIcon.css";
import edit_icon from "../../../../images/edit_icon.svg";
import { ModalConfirm, ModalError } from "../../../../components/Common/Modals/Modals.jsx";
import StockModalTemplate from "../../../../components/StockModalTemplate/StockModalTemplate.jsx";
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { AlertDanger } from "../../../../components/Common/Alerts/Alerts.jsx";
import { OnlyInteger } from "../../../../Utils/InputText.js";



const EditStockIcon = ({ portfolioQuote, refreshTable }) => {
    const [showModal, setShowModal] = useState(false);
    const [portfolioData, setPortfolioData] = useState({});
    const [showErrorAlert, setShowErrorAlert] = useState(false);
    const [errors400Response, setErrors400Response] = useState([]);
    //Modal Error
    const [showErrorModal, setShowErrorModal] = useState(false);
    const [ErrorMessage, setErrorMessage] = useState("");


    useEffect(() => {
        if (showModal) {
            setPortfolioData({ quoteId: portfolioQuote.id, quantity: portfolioQuote._Portfolio.quantity, price: portfolioQuote._Portfolio.averagePurchasePrice });
            setErrors400Response([]);
            setShowErrorAlert(false);
        }
    }, [showModal, portfolioQuote]);


    const CreateErrorContent = () => {
        return (
            errors400Response.map(err => (<div>{err}</div>))
        )
    }


    const CreateEditModalContent = (pQuote) => {
        //Evento para cantidad de acciones
        const handleQuantityInput = (event) => {
            const quantityvalue = event.target.value;
            //Solo permite numeros enteros
            if (OnlyInteger(quantityvalue)) {
                setPortfolioData({ ...portfolioData, quantity: quantityvalue });
            }
        }

        //evento para precio de venta promedio
        const handlePriceInput = (event) => {
            setPortfolioData({ ...portfolioData, price: event.target.value });
        }


        const stockEditContent = (
            <>
                <Form>
                    <Form.Group as={Row} className="mb-3" controlId="formHorizontalQuantity">
                        <Form.Label column sm={2}>
                            Cantidad
                        </Form.Label>
                        <Col sm={10}>
                            <Form.Control
                                type="text"
                                value={portfolioData.quantity}
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
                                value={portfolioData.price}
                                onChange={handlePriceInput}
                            />
                        </Col>
                    </Form.Group>

                </Form>
                <AlertDanger show={showErrorAlert} content={CreateErrorContent()} />
            </>
        )

        return (
            <StockModalTemplate
                quote={pQuote}
                content={stockEditContent}
            />
        )
    }

    const modalContent = CreateEditModalContent(portfolioQuote);


    //Abre el modal de error
    function openErrorModal(message) {
        setErrorMessage(message);
        setShowErrorModal(true);
    }


    function UpdatePortfolioStock() {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                'quoteId': portfolioData.quoteId,
                'quantity': portfolioData.quantity,
                'price': portfolioData.price
            })
        };

        fetch("api/Portfolio/UpdatePortfolioStock", requestOptions)
            .then(async response => {
                switch (response.status) {
                    case 200: //OK
                        setShowModal(false);
                        refreshTable();
                        break;
                    case 400: //BAD REQUEST
                        const text400 = await response.text();
                        const jsonErrors = JSON.parse(text400).errors;
                        //Agregamos los nuevos errores
                        setErrors400Response(Object.values(jsonErrors));
                        //Mostramos el alert
                        setShowErrorAlert(true);
                        break;
                    default:
                        //Agregamos los nuevos errores
                        setErrors400Response(["Ocurrió un ERROR al actualizar el portfolio"]);
                        //Mostramos el alert
                        setShowErrorAlert(true);
                        break;
                }
            })
            .catch(error => {
                //Modal de ERROR
                openErrorModal("Ocurrió un ERROR al actualizar el portfolio");
            })
    }


    return (
        <>
            <img className="btnEdit" src={edit_icon} alt="Editar" width="32" title="Editar" onClick={() => { setShowModal(true) }} />
            <ModalConfirm
                show={showModal}
                content={modalContent}
                buttonNoTitle="Cancelar"
                onButtonNoClick={() => setShowModal(false)}
                buttonYesTitle="Confirmar"
                onButtonYesClick={UpdatePortfolioStock}
            />

            <ModalError
                show={showErrorModal}
                content={ErrorMessage}
                onClose={() => setShowErrorModal(false)}
            />
        </>
    )

}

export default EditStockIcon;