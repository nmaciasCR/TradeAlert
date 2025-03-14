﻿import React, { useEffect, useState } from "react";
import Styles from "./IndexMarket.css";
import arrowUp from "../../images/quote_arrow_up.png";
import arrowDown from "../../images/quote_arrow_down.png";
import arrowZero from "../../images/quote_arrow_zero.png";
import { Trunc2Decimal } from "../../Utils/Numbers.js";
import { Link } from 'react-router-dom';


function GetArrow(pricePercent) {
    switch (Math.sign(pricePercent)) {
        case 1:
            return (arrowUp);
        case -1:
            return (arrowDown);
        default:
            return (arrowZero);
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


const Indexs = () => {

    const [mainstocksList, setMainStockList] = useState([]);

    useEffect(() => {
        fetch("api/Stocks/GetMainStocks")
            .then(response => { return response.json() })
            .then(responseJson => {
                setMainStockList(responseJson);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);



    return (
        <div className="indexMarketContainer">
            {
                mainstocksList.map(q =>
                (
                    <Link to={`/Quote?q=${q.symbol}`} className="block-link" key={q.id}>
                        <div key={q.id} className="block">
                            <div className="symbol" title={q.name}>{q.symbol}</div>
                            <div className="arrow"><img src={GetArrow(q.regularMarketChangePercent)} height="20px" alt="" /></div>
                            <div className={`quote ${GetQuoteClass(q.regularMarketChangePercent)}`}>{Trunc2Decimal(q.regularMarketChangePercent)} %</div>
                        </div>
                    </Link>
                )
                )
            }
        </div>
    )
}


export default Indexs;