import React from "react";
import Styles from "./ActionsContainer.css";
import ActionAlert from "../../../../components/QuoteBell/QuoteBell";



const ActionContainer = (props) => {

    return (
        <div className="Action-Container">
            <ActionAlert quote={props.quote} afterCloseModal={props.refreshTable} />
        </div>
    )
}


export default ActionContainer;