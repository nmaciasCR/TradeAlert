import React, { useEffect, useState, useCallback } from "react";
import Styles from './Alerts.css';
import StockAlertsContent from "../../../../components/StockAlertsContent/StockAlertsContent";



const Alerts = ({ quote }) => {


    return (
        <div className="alerts-container">
            <div className="title">Alertas</div>
            <div className="">
                <StockAlertsContent quote={quote} />


            </div>
        </div>
    )
}


export default Alerts;