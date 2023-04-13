import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';


function Modalinfo({ show, onClose, content }) {

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

export default Modalinfo;