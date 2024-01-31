import React, { useState } from "react";
import Styles from "./CardsOptions.css";
import Dropdown from 'react-bootstrap/Dropdown';
import dots3 from "../../../../images/3dots.png";
import { ModalConfirm } from "../../../../components/Common/Modals/Modals.jsx";



const CardsOptions = ({ id, active, refresList }) => {

    const [showDeleteConfirmModal, setDeleteConfirmModal] = useState(false);

    //Retorna el nombre de la accion Marcar como leído /  Marcar como no leído
    const GetSetNotificationActiveName = (active) => {
        return active ? "Marcar como leído" : "Marcar como no leído";
    }


    //CAmbia el estado de la notificacion
    function SetNotificationActive(id, active, refreshNotificationList) {

        fetch(`api/Notifications/SetActive?id=${id}&active=${active}`, { method: 'POST' })
            .then(response => {
                refreshNotificationList();
            })
            .catch(error => {
                //Modal ERROR
                console.log(error);
            })
    }


    //Elimina una notificacion
    function DeleteNotification(id, refreshNotificationList) {
        fetch(`api/Notifications/Delete?id=${id}`, { method: 'DELETE' })
            .then(response => {
                refreshNotificationList();
            })
            .catch(error => {
                //Modal ERROR
                console.log(error);
            })
    }


    return (
        <>
            <Dropdown className="d-inline mx-2">
                <Dropdown.Toggle id="dropdown-autoclose-true" bsPrefix="dropdown-toggle dropdown-custom-toggle" as="span">
                    <img src={dots3} width="45px" alt="Acciones" />
                </Dropdown.Toggle>

                <Dropdown.Menu>
                    <Dropdown.Item href="#" onClick={() => SetNotificationActive(id, !active, refresList)}>{GetSetNotificationActiveName(active)}</Dropdown.Item>
                    <Dropdown.Item href="#" onClick={() => setDeleteConfirmModal(true)}>Eliminar</Dropdown.Item>
                </Dropdown.Menu>
            </Dropdown>
            <ModalConfirm
                show={showDeleteConfirmModal}
                content="¿Desea eliminar esta notificacion?"
                buttonNoTitle="No"
                onButtonNoClick={() => setDeleteConfirmModal(false)}
                buttonYesTitle="Si"
                onButtonYesClick={() => DeleteNotification(id, refresList)}
            />
        </>
    )
}


export default CardsOptions;