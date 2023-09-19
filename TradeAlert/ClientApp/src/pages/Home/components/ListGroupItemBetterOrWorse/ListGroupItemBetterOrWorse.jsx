import React from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Styles from "./ListGroupItemBetterOrWorse.css";
import arrowUp from "../../../../images/quote_arrow_up.png";
import arrowDown from "../../../../images/quote_arrow_down.png";
import arrowZero from "../../../../images/quote_arrow_zero.png";



const imgPath = require.context('../../../../images/flags', true);



const getFlag = (flag, title) => {
    return (<img src={imgPath(`./` + flag)} title={title} width="26px" />)
}

function GetArrow(pricePercent) {
    switch (Math.sign(pricePercent)) {
        case 1:
            return arrowUp;
        case -1:
            return arrowDown;
        default:
            return arrowZero;
    }

}


function GetQuoteClass(pricePercent) {
    switch (Math.sign(pricePercent)) {
        case 1:
            return "quote-up";
        case -1:
            return "quote-down";
        default:
            return "quote-zero";
    }

}


const ListGroupItem = ({ quote }) => {

    return (<ListGroup.Item>
        <div className="itemListContainer">
            <div className="flag">{getFlag(quote._market.flag, quote._market.description)}</div>
            <div className="symbol" title={quote.name}>{quote.symbol}</div>
            <div className="imgArrow"><img src={GetArrow(quote.regularMarketChangePercent)} width="20px" /></div>
            <div className={`quote ${GetQuoteClass(quote.regularMarketChangePercent)}`}>{quote.regularMarketChangePercent} %</div>
        </div>
    </ListGroup.Item>)
}

export default ListGroupItem;