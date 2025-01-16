import React, { useEffect, useState, useCallback } from "react";
import Styles from './GroupsHeader.css';
import GroupsHeaderButton from '../GroupsHeaderButton/GroupsHeaderButton';
import GroupsHeaderDescription from '../GroupsHeaderDescription/GroupsHeaderDescription';


const GroupsHeader = ({ quoteId, quoteName, quoteGroups, fnLoadQuote }) => {
    let anyGroup = quoteGroups.length == 0;

    return (
        <>
        
            {/*Mostramos el componente indicado segun si la accion tiene grupos o no*/}
            {anyGroup ? <GroupsHeaderButton quoteId={quoteId} quoteName={quoteName} fnLoadQuote={fnLoadQuote} />
                : <GroupsHeaderDescription quoteId={quoteId} quoteName={quoteName} fnLoadQuote={fnLoadQuote}  groups={quoteGroups} />}
        </>
    )
}


export default GroupsHeader;