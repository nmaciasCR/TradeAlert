import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Styles from "./Modals.css";


export function ModalInfo({ show, onClose, content }) {

    return (
        <Modal show={show} onHide={onClose}>
            <Modal.Header closeButton>
                <Modal.Title>Alertas</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {content}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={onClose}>
                    Cerrar
                </Button>
            </Modal.Footer>
        </Modal>
    );

}


export function ModalConfirm({ show, content, buttonNoTitle, onButtonNoClick, buttonYesTitle, onButtonYesClick }) {

    return (
        <Modal show={show} onHide={onButtonNoClick}>
            <Modal.Header closeButton></Modal.Header>
            <Modal.Body>
                {content}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onButtonNoClick}>
                    {buttonNoTitle}
                </Button>
                <Button variant="primary" onClick={onButtonYesClick}>
                    {buttonYesTitle}
                </Button>
            </Modal.Footer>
        </Modal>
    );
}


export function ModalError({ show, content, onClose }) {

    return (
        <Modal
            show={show}
            onHide={onClose}
            backdrop="static"
            keyboard={false}
        >
            <Modal.Header bsPrefix="modal-header modal-error-header" closeButton>
                <Modal.Title>Error</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {content}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={onClose}>
                    Cerrar
                </Button>
            </Modal.Footer>
        </Modal>
    );
}