import React from "react";
import styles from "./QuoteBlock.css";
import BellIcon from "../QuoteBell/QuoteBell";
import { Trunc2Decimal } from "../../Utils/Numbers.js";




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
                <span className="blkPrice">   {Trunc2Decimal(props.quote.regularMarketPrice)}</span>
                <span className="blkCurrency">  {props.quote.currency}</span>
                <div className="bellIconContainer">
                    <BellIcon quote={props.quote} />
                </div>
            </div>
            <div className="blkCompanyName" title={props.quote.name}>{props.quote.name}</div>
            <div className="blkRegularMarketPrices">
                <div className="quote-arrow"></div>
                <span className="blkMarketPrice">{Trunc2Decimal(props.quote.regularMarketChangePercent)}% ({Trunc2Decimal(props.quote.regularMarketChange)})</span>
                <span></span>
            </div>
        </div>
    );

}

export default Block;