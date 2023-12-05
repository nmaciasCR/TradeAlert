import React, { useState, useEffect } from "react";
import Styles from "./EditStockIcon.css";
import edit_icon from "../../../../images/edit_icon.svg";
import { ModalConfirm, ModalError } from "../../../../components/Common/Modals/Modals.jsx";
import StockModalTemplate from "../../../../components/StockModalTemplate/StockModalTemplate.jsx";
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { AlertDanger } from "../../../../components/Common/Alerts/Alerts.jsx";



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
            setPortfolioData({ quoteId: portfolioQuote.quoteId, quantity: portfolioQuote.quantity });
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
        const handleQuantityInput = (event) => {
            setPortfolioData({ ...portfolioData, quantity: event.target.value });
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
                                type="number"
                                value={portfolioData.quantity}
                                onChange={handleQuantityInput}
                            />
                        </Col>
                    </Form.Group>
                </Form>
                <AlertDanger show={showErrorAlert} content={CreateErrorContent()} />
            </>
        )

        return (
            <StockModalTemplate
                quote={pQuote._quote}
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
                'quantity': portfolioData.quantity
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
                        const jsonErrors = JSON.parse(text400);
                        //Agregamos los nuevos errores
                        setErrors400Response([jsonErrors.errors.quantity]);
                        //Mostramos el alert
                        setShowErrorAlert(true);
                        break;
                    default:
                        const textError = await response.text();
                        throw new Error(textError);
                        break;
                }
            })
            .catch(error => {
                //Modal de ERROR
                openErrorModal(error.toString());
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