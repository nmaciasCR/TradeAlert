import React from "react";
import Styles from "./StockItem.css";
import InfoIcon from "../../../images/info_red_icon.png";
import highPriorityIcon from "../../../images/high_priority_icon.png";
import mediumPriorityIcon from "../../../images/medium_priority_icon.png";
import lowPriorityIcon from "../../../images/low_priority_icon.png";
import { format } from 'date-fns';



const imgPath = require.context('../../../images/flags', true);

function GetArrowClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "quote-arrow-up";
        case -1:
            return "quote-arrow-down";
        default:
            return "quote-arrow-zero";
    }
}

function GetPriceClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "price-up";
        case -1:
            return "price-down";
        default:
            return "price-zero";
    }
}

function GetPriorityIcon(priorityId) {
    switch (priorityId) {
        case 1: return (<img src={highPriorityIcon} title="Alta Prioridad" width="26px" alt="Alta Prioridad" />);
        case 2: return (<img src={mediumPriorityIcon} title="Media Prioridad" width="26px" alt="Media Prioridad" />);
        case 3: return (<img src={lowPriorityIcon} title="Baja Prioridad" width="26px" alt="Baja Prioridad" />);
        default: return null;
    }
}

export const StockItem = {
    symbol: (ticker) => {
        return (<span className="StockItem-ticker">{ticker}</span>);
    },
    market: (flag, title) => {
        return (<img src={imgPath(`./` + flag)} title={title} width="26px" alt={title} />);
    },
    arrow: (price) => {
        let arrowClass = GetArrowClass(price);
        return (
            <div className={arrowClass}></div>
        );
    },
    changePercent: (price) => {
        let priceClass = GetPriceClass(price);
        return (
            <div className="StockItem-price-container">
                {StockItem.arrow(price)}
                <div className={priceClass}>{price}</div>
            </div>
        );
    },
    change: (price) => {
        let priceClass = GetPriceClass(price);
        return (
            <div className="StockItem-price-container">
                <div className={priceClass}>{price}</div>
            </div>
        );
    },
    infoRedIcon: (showElement) => {
        return (showElement && <img src={InfoIcon} title="Revisión Requerida" width="22px" alt="Revisión Requerida" />)
    },
    priorityIcon: (priorityId) => {
        return GetPriorityIcon(priorityId);
    },
    lastReview: (daysDiff, dateReview) => {
        var dateRev = format(new Date(dateReview), 'dd/MM/yyyy')
        return (<span title={dateRev}>{daysDiff} Días</span>)
    }
}
