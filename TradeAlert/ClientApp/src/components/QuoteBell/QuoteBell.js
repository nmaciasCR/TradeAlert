import React, { useState } from "react";
import styles from "./QuoteBell.css";
import bellIcon from "../../images/bell_icon.png";
import bellIconActive from "../../images/bell_icon_active.png";
import { createPortal } from 'react-dom';
import ModalInfo from "../Common/Modals/ModalInfo.jsx";
import ModalContent from "../StockModalContent/StockModalContent.jsx";


const QuoteBell = (props) => {
    const [showModal, setShowModal] = useState(false);

    const handleShow = () => setShowModal(true);
    const handleClose = () => {
        setShowModal(false);
        //Accion despues de cerrar el modal
        if (props.afterCloseModal != undefined) {
            props.afterCloseModal();
        }
    }


    return (
        <>
            <img className="bell-icon" src={bellIcon} onClick={() => setShowModal(true)} />
            <ModalInfo
                show={showModal}
                onClose={handleClose}
                content={<ModalContent quote={props.quote} />}
            />
        </>
    )

}


export default QuoteBell;