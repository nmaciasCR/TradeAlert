import React, { useState, useEffect } from "react";
import deleteIcon from "../../../../images/cancel_Icon.png";
import Styles from "./DeleteStockIcon.css";
import { ModalConfirm, ModalError } from "../../../../components/Common/Modals/Modals.jsx";
import { AlertDanger } from "../../../../components/Common/Alerts/Alerts.jsx";


const DeleteStockIcon = ({ quoteId, name, refreshTable }) => {
    const [showModal, setShowModal] = useState(false);
    //Alert
    const [showErrorAlert, setShowErrorAlert] = useState(false);
    const [dangerAlertContent, setDangerAlertContent] = useState([]);
    //Modal Error
    const [showErrorModal, setShowErrorModal] = useState(false);
    const [ErrorMessage, setErrorMessage] = useState("");

    useEffect(() => {
        if (showModal) {
            setDangerAlertContent([]);
            setShowErrorAlert(false);
        }
    }, [showModal]);


    const contentConfirmModal = () => {
        return (
            <>
                <div>
                    ¿Está seguro de querer quitar a '<strong>{name}</strong>' del portfolio ?
                </div>
                <AlertDanger show={showErrorAlert} content={dangerAlertContent} />
            </>
        )
    }

    //Abre el modal de error
    function openErrorModal(message) {
        setErrorMessage(message);
        setShowErrorModal(true);
    }



    //Eliminamos una accion del portfolio
    function DeletePortfolioQuote() {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        };

        fetch(`api/Portfolio/DeleteStockPortfolio?idPortfolio=${quoteId}`, requestOptions)
            .then(async response => {
                switch (response.status) {
                    case 200: //OK
                        setShowModal(false);
                        refreshTable();
                        break;
                    case 400: //BAD REQUEST
                        const text400 = await response.text();
                        //Agregamos los nuevos errores
                        setDangerAlertContent([text400]);
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
            <img
                src={deleteIcon}
                className="delete-icon"
                title="Quitar del Portfolio"
                alt="Quitar del Portfolio"
                width="32"
                onClick={() => setShowModal(true)}
            />
            <ModalConfirm
                show={showModal}
                content={contentConfirmModal()}
                buttonNoTitle="No"
                onButtonNoClick={() => setShowModal(false)}
                buttonYesTitle="Eliminar"
                onButtonYesClick={() => DeletePortfolioQuote()}
            />

            <ModalError
                show={showErrorModal}
                content={ErrorMessage}
                onClose={() => setShowErrorModal(false)}
            />

        </>
    )
}


export default DeleteStockIcon;