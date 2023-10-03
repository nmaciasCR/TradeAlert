import React, { useEffect, useState } from "react";
import Styles from "./IndexMarket.css";
import arrowUp from "../../images/quote_arrow_up.png";
import arrowDown from "../../images/quote_arrow_down.png";
import arrowZero from "../../images/quote_arrow_zero.png";

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



    return (<div className="indexMarketContainer">

        {
            mainstocksList.map(q =>
            (<div key={q.id} className="block">
                <div  className="symbol" title={q.name}>{q.symbol}</div>
                <div className="arrow"><img src={GetArrow(q.regularMarketChangePercent)} height="22px" alt=""/></div>
                <div className={`quote ${GetQuoteClass(q.regularMarketChangePercent)}`}>{q.regularMarketChangePercent} %</div>
            </div>)
            )

        }

    </div>)
}


export default Indexs;