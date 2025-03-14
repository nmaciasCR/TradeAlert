﻿import React, { useEffect, useState } from "react";
import ListGroup from 'react-bootstrap/ListGroup';
import arrowUp from "../../images/quote_arrow_up.png";
import arrowDown from "../../images/quote_arrow_down.png";
import arrowZero from "../../images/quote_arrow_zero.png";
import Styles from "./PortfolioTable.css";
import { Trunc2Decimal } from "../../Utils/Numbers";
import { Link } from 'react-router-dom';




const imgPath = require.context('../../images/flags', true);

const getFlag = (flag, title) => {
    return (<img src={imgPath(`./` + flag)} title={title} width="22px" alt={title} />)
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


const portfolioStockItem = (quote) => {

    return (
        <ListGroup.Item key={quote.id}>
            <div className="itemPortfolioContainer">
                <div className="symbolContainer">
                    <div className="flag">{getFlag(quote._market.flag, quote._market.description)}</div>
                    <div className="symbol" title={quote.name}>
                        <Link className="quote-link" to={`/Quote?q=${quote.symbol}`}>{quote.symbol}</Link>
                    </div>
                </div>
                <div className="quoteContainer">
                    <div className="imgArrow"><img src={GetArrow(quote.regularMarketChangePercent)} width="16px" /></div>
                    <div className={`quote ${GetQuoteClass(quote.regularMarketChangePercent)}`}>{Trunc2Decimal(quote.regularMarketChangePercent)} %</div>
                </div>
            </div>
        </ListGroup.Item>
    )

}


const PortfolioTable = () => {
    const [portfolioList, setPortfolioList] = useState([]);


    useEffect(() => {
        fetch("api/Portfolio/GetPortfolio")
            .then(response => { return response.json() })
            .then(responseJson => {
                setPortfolioList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });


    }, []);



    return (
        <ListGroup className="PortfolioContainer">
            <ListGroup.Item variant=""><h4>Portafolio</h4></ListGroup.Item>
            {
                portfolioList.sort((a, b) => a.name > b.name ? 1 : -1)
                    .map(p => (portfolioStockItem(p)))
            }
            <ListGroup.Item className="linkToPortfolio">
                <Link to="/Portfolio">Ir a Portfolio</Link>
            </ListGroup.Item>
        </ListGroup>
    )
}


export default PortfolioTable;