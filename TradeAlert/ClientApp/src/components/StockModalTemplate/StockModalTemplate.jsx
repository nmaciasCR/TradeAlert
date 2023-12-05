import React from "react";
import styles from "./StockModalTemplate.css";
import { Trunc2Decimal } from "../../Utils/Numbers.js";
import arrow_up from "../../images/quote_arrow_up.png";
import arrow_down from "../../images/quote_arrow_down.png";
import arrow_zero from "../../images/quote_arrow_zero.png";


const imgPath = require.context('../../images/flags', true);


function containerClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "changePercent-up";
        case -1:
            return "changePercent-down";
        default:
            return "changePercent-zero";
    }
}

function getArrow(percent) {
    switch (Math.sign(percent)) {
        case 1:
            return (<img src={arrow_up} width="22" alt="" />);
        case -1:
            return (<img src={arrow_down} width="22" alt="" />);
        default:
            return (<img src={arrow_zero} width="22" alt="" />);
    }
}


function StockModalTemplate({ quote, content }) {
    const cssname = containerClass(quote.regularMarketChangePercent); //verde o rojo
    const arrowPercent = getArrow(quote.regularMarketChangePercent);

    return (
        <div className="ModalTemplate">
            <div className="title">
                <img className="flag" src={imgPath(`./` + quote._market.flag)} title={quote._market.description} alt={quote._market.description} />
                <div className="symbol">{quote.symbol}</div>
                <div className={`changePercent ${cssname}`}>
                    <div className="quote-arrow">
                        {arrowPercent}
                    </div>
                    {Trunc2Decimal(quote.regularMarketChangePercent)} % ({Trunc2Decimal(quote.regularMarketChange)})
                </div>
            </div>
            <div className="name">{quote.name}</div>
            {content}
        </div>
    )
}


export default StockModalTemplate;