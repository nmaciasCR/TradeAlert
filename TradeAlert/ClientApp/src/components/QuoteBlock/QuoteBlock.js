import React from "react";
import styles from "./QuoteBlock.css";
import BellIcon from "../QuoteBell/QuoteBell";




function containerClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "blockContainer-up";
        case -1:
            return "blockContainer-down";
        default:
            return "blockContainer-zero";
    }
}

function Block(props) {
    const cssname = containerClass(props.quote.regularMarketChangePercent);

    return (
        <div className={`blockContainer ${cssname}`}>
            <div>
                <span className="blkSymbol">{props.quote.symbol}</span>
                <span className="blkPrice">   {props.quote.regularMarketPrice}</span>
                <span className="blkCurrency">  {props.quote.currency}</span>
                <BellIcon quote={ props.quote } />
            </div>
            <div className="blkCompanyName">{props.quote.name}</div>
            <div className="blkRegularMarketPrices">
                <div className="quote-arrow"></div>
                <span className="blkMarketPrice">{props.quote.regularMarketChangePercent}% ({props.quote.regularMarketChange})</span>
                <span></span>
            </div>
        </div>
    );

}

export default Block;