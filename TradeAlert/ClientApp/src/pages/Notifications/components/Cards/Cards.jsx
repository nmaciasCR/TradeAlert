import React from "react";
import Styles from "./Cards.css";
import supportIcon from "../../../../images/notification_down_icon.png";
import resistorIcon from "../../../../images/notification_up_icon.png";
import earningIcon from "../../../../images/notification_earning_icon.png";
import moment from "moment";
import 'moment/locale/es';
import CardsOptions from "../CardsOptions/CardsOptions.jsx";


var momentLib = require('moment');


//Retorna el tiempo desde una determinada fecha hasta ahora
const GetTimeOf = (date) => {
    return momentLib(date, momentLib.ISO_8601).fromNow();
}


//Notificacion de soporte
export const Support = ({ id, entryDate, title, description, active, refresList }) => {
    return (
        <div key={id} className={`Box-Notifications ${(active) ? 'Box-Notifications-active' : ''}`}>
            <div className="icon-container">
                <img src={supportIcon} alt="Soporte" title="Soporte" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
            <div className="actions-container">
                <CardsOptions
                    id={id}
                    active={active}
                    refresList={refresList}
                />
            </div>
        </div>
    )
}


//Notificacion de resistencia
export const Resistor = ({ id, entryDate, title, description, active, refresList }) => {
    return (
        <div key={id} className={`Box-Notifications ${(active) ? 'Box-Notifications-active' : ''}`}>
            <div className="icon-container">
                <img src={resistorIcon} alt="Resistencia" title="Resistencia" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
            <div className="actions-container">
                <CardsOptions
                    id={id}
                    active={active}
                    refresList={refresList}
                />
            </div>
        </div>
    )
}


//Notificacion de Resultados 3 dias
export const Earnings3Days = ({ id, entryDate, title, description, active, refresList }) => {
    return (
        <div key={id} className={`Box-Notifications ${(active) ? 'Box-Notifications-active' : ''}`}>
            <div className="icon-container">
                <img src={earningIcon} alt="Resultados en 3 días" title="Resultados en 3 días" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
            <div className="actions-container">
                <CardsOptions
                    id={id}
                    active={active}
                    refresList={refresList}
                />
            </div>
        </div>
    )
}


//Notificacion de Resultados hoy
export const EarningsToday = ({ id, entryDate, title, description, active, refresList }) => {
    return (
        <div key={id} className={`Box-Notifications ${(active) ? 'Box-Notifications-active' : ''}`}>
            <div className="icon-container">
                <img src={earningIcon} alt="Resultados hoy" title="Resultados hoy" className="icon-notification" />
            </div>
            <div className="Card-description">
                <div className="Date">{GetTimeOf(entryDate)}</div>
                <div className="Ticker">{title}</div>
                <div>{description}</div>
            </div>
            <div className="actions-container">
                <CardsOptions
                    id={id}
                    active={active}
                    refresList={refresList}
                />
            </div>
        </div>
    )
}

