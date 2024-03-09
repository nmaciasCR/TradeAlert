import React from "react";
import Styles from "./StockItem.css";
import InfoIcon from "../../../images/info_red_icon.png";
import highPriorityIcon from "../../../images/high_priority_icon.png";
import mediumPriorityIcon from "../../../images/medium_priority_icon.png";
import lowPriorityIcon from "../../../images/low_priority_icon.png";
import { format } from 'date-fns';
import portfolioIcon from "../../../images/portfolio_brown_icon.png";




const imgPath = require.context('../../../images/flags', true);

function GetArrowClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "quote-arrow quote-arrow-up";
        case -1:
            return "quote-arrow quote-arrow-down";
        default:
            return "quote-arrow quote-arrow-zero";
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
    number: (num, withArrow) => {
        let priceClass = GetPriceClass(num);
        return (
            <div className="StockItem-value-container">
                {withArrow? StockItem.arrow(num): ''}
                <div className={priceClass}>{num}</div>
            </div>
        );
    },
    profit: (num, percent) => {
        let priceClass = GetPriceClass(num);
        return (
            <div className="StockItem-value-container">
                {StockItem.arrow(num)}
                <div className={priceClass}>{num}</div>&nbsp;
                <div className={priceClass}>({percent} %)</div>
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
    },
    portfolioIcon: (showElement) => {
        return (showElement && <img src={portfolioIcon} className="name-table-icon" width="20" title="En Portafolio" alt="En Portafolio" />)
    }
}
