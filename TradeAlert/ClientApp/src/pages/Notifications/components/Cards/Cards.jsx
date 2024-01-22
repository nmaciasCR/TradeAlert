import React from "react";
import Styles from "./Cards.css";
import supportIcon from "../../../../images/notification_down_icon.png";
import resistorIcon from "../../../../images/notification_up_icon.png";
import moment from "moment";
import 'moment/locale/es';




var momentLib = require('moment');


//Retorna el tiempo desde una determinada fecha hasta ahora
const GetTimeOf = (date) => {
    return momentLib(date, momentLib.ISO_8601).fromNow();
}




//Notificacion de soporte
export const Support = ({ id, entryDate, title, description, active }) => {
    return (
        <div key={id} className="Box-Notifications">
            <div>
                <img src={supportIcon} alt="Soporte" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
        </div>
    )
}


//Notificacion de resistencia
export const Resistor = ({ id, entryDate, title, description, active }) => {
    return (
        <div key={id} className="Box-Notifications">
            <div>
                <img src={resistorIcon} alt="Resistencia" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
        </div>
    )
}

