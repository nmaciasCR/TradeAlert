import React, { useState } from "react";
import styles from "./QuoteBell.css";
import bellIcon from "../../images/bell_icon.png";
import bellIconActive from "../../images/bell_icon_active.png";
import { ModalInfo } from "../Common/Modals/Modals";
import AlertsContent from "../StockAlertsContent/StockAlertsContent.jsx";
import StockModalTemplate from "../StockModalTemplate/StockModalTemplate.jsx";



function GetBellIcon(isActive) {
    if (isActive) {
        return bellIconActive;
    } else {
        return bellIcon;
    }
}


const QuoteBell = (props) => {
    const [showModal, setShowModal] = useState(false);

    //const handleShow = () => setShowModal(true);
    const handleClose = () => {
        setShowModal(false);
        //Accion despues de cerrar el modal
        if (props.afterCloseModal !== undefined) {
            props.afterCloseModal();
        }
    }

    //El content para el modal de las alertas
    //usando el template de StockModalTemplate
    const CreateAlertModalContent = (quote) => {

        return (
            <StockModalTemplate
                quote={quote}
                content={<AlertsContent quote={quote} />}
            />
        )
    }

    return (
        <>
            <img className="bell-icon" src={GetBellIcon(props.quote.reviewRequired)} onClick={() => setShowModal(true)} alt="Alertas" title="Alertas" />
            <ModalInfo
                show={showModal}
                onClose={handleClose}
                content={CreateAlertModalContent(props.quote)}
            />
        </>
    );
}


export default QuoteBell;