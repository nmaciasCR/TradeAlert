import React, { useEffect, useState, useCallback } from "react";
import Styles from './GroupsHeaderButton.css';
import Button from 'react-bootstrap/Button';
import StockGroupsAdminModal from '../../../../components/StockGroupsAdminModal/StockGroupsAdminModal';



const GroupsHeaderButton = ({ quoteId, quoteName, fnLoadQuote }) => {
    const [showGroupsModal, setShowGroupsModal] = useState(false);

    //Se ejecuta cuando cerramos el modal
    const fnClose = () => {
        setShowGroupsModal(false);
    }

    return (
        <>
            <StockGroupsAdminModal
                show={showGroupsModal}
                quoteId={quoteId}
                quoteName={quoteName}
                onclose={fnClose}
                fnLoadQuote={fnLoadQuote}
            />
            <Button variant="outline-secondary" size="sm" onClick={() => setShowGroupsModal(true)}>Agregar Grupos</Button>
        </>
    )
}


export default GroupsHeaderButton;