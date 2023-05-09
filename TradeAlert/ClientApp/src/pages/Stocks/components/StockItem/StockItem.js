import React from "react";
import Styles from "./StockItem.css";
import InfoIcon from "../InfoRedIcon/InfoRedIcon";




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


export const StockItem = {
    symbol: (ticker) => {
        return (<span className="StockItem-ticker">{ticker}</span>);
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
        return (showElement && <InfoIcon title="Revisión Requerida" />)
    }
}
