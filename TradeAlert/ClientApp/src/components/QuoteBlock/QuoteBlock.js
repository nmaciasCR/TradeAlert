import React from "react";
import styles from "./QuoteBlock.css";
import BellIcon from "../QuoteBell/QuoteBell";
import { Trunc2Decimal } from "../../Utils/Numbers.js";
import portfolioIcon from "../../images/portfolio_brown_icon.png";




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

function portfoilioIcon(showIcon) {
    return (showIcon && <img className="iconContainer" src={portfolioIcon} alt="Portafolio" title="En Portfafolio" width="22" />)
}

function Block(props) {
    const cssname = containerClass(props.quote.regularMarketChangePercent);

    return (
        <div className={`blockContainer ${cssname}`}>
            <div>
                <span className="blkSymbol">{props.quote.symbol}</span>
                <span className="blkPrice">   {Trunc2Decimal(props.quote.regularMarketPrice)}</span>
                <span className="blkCurrency">  {props.quote._currency.code}</span>
                <div className="blockIconContainer">
                    <BellIcon quote={props.quote} />
                    {portfoilioIcon(props.quote._Portfolio != null)}
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