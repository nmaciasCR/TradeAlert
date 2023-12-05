import React from 'react';
import Alert from 'react-bootstrap/Alert';


export function AlertDanger({ show, content }) {

    return (
        <Alert key="danger" variant="danger" show={show}>
            {content}
        </Alert>
    )
}