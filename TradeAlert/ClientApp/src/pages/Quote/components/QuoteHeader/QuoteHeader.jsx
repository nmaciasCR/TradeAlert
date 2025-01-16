import React, { useEffect, useState, useCallback } from "react";
import Styles from './QuoteHeader.css';
import { GetArrowForPrice } from '../../../../Utils/Images';
import { Trunc2Decimal } from '../../../../Utils/Numbers';
import GroupsHeader from '../GroupsHeader/GroupsHeader';

const imgPath = require.context('../../../../images/flags', true);

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

const QuoteHeader = ({ quote, fnLoadQuote }) => {

    return (
        <div className="Header">
            <div className="name-quotes">
                <img className="flag" src={imgPath(`./` + quote._market.flag)} title={quote._market.description} alt={quote._market.description} />
                <span className="name">{quote.name} ({quote.symbol})</span>
                <span className="currency">{quote._currency.code}</span>
                <span className="price">{Trunc2Decimal(quote.regularMarketPrice)}</span>
            </div>

            <div className="market-quotes">
                <div className='arrow'><img className="" src={GetArrowForPrice(quote.regularMarketChangePercent)} title='' alt='' /></div>
                <div className={`change ${GetQuoteClass(quote.regularMarketChange)}`}>{Trunc2Decimal(quote.regularMarketChange)}</div>
                <div className={`change-percent ${GetQuoteClass(quote.regularMarketChangePercent)}`}>({Trunc2Decimal(quote.regularMarketChangePercent)} %)</div>
            </div>

            <div className="group-description">
                <GroupsHeader quoteId={quote.id} quoteName={quote.name} quoteGroups={quote._groups} fnLoadQuote={fnLoadQuote} />
            </div>

        </div >
    )

}

export default QuoteHeader;