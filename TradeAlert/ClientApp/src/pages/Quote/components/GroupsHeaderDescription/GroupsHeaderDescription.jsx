import React, { useEffect, useState, useCallback } from "react";
import Styles from './GroupsHeaderDescription.css';
import StockGroupsAdminModal from '../../../../components/StockGroupsAdminModal/StockGroupsAdminModal';


const GroupsHeaderDescription = ({ quoteId, quoteName, fnLoadQuote, groups }) => {
    const [showGroupsModal, setShowGroupsModal] = useState(false);

    let groupString = groups.map(item => item.description).join(' • ');

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
            <span className="grops-names" onClick={() => setShowGroupsModal(true)}>{groupString}</span>
        </>
    )
}


export default GroupsHeaderDescription;