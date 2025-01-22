import React, { useEffect, useState } from "react";
import ListGroup from 'react-bootstrap/ListGroup';
import Styles from "./StockTableBetterOrWorse.css";
import { Trunc2Decimal } from "../../Utils/Numbers.js";
import portfolioIcon from "../../images/portfolio_brown_icon.png";
import { Link } from 'react-router-dom';
import { GetArrowForPrice } from '../../Utils/Images';



const imgPath = require.context('../../images/flags', true);


const getFlag = (flag, title) => {
    return (<img src={imgPath(`./` + flag)} title={title} width="22px" />)
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

function GetPortfolioIcon(showIcon) {
    return showIcon && (<img src={portfolioIcon} title="En Potafolio" width="16px" />)
}


const stockItem = (quote) => {

    return (
        <ListGroup.Item key={quote.id}>
            <div className="itemListContainer">
                <div className="symbolContainer">
                    <div className="flag">{getFlag(quote._market.flag, quote._market.description)}</div>
                    <div className="symbol" title={quote.name}>
                        <Link to={`/Quote?q=${quote.symbol}`} className="quote-link">{quote.symbol}</Link>
                    </div>
                    <div className="portfolioIcon">{GetPortfolioIcon(quote._Portfolio !== null)}</div>
                </div>
                <div className="quoteContainer">
                    <div className="imgArrow"><img src={GetArrowForPrice(quote.regularMarketChangePercent)} width="16px" /></div>
                    <div className={`quote ${GetQuoteClass(quote.regularMarketChangePercent)}`}>{Trunc2Decimal(quote.regularMarketChangePercent)} %</div>
                </div>
            </div>
        </ListGroup.Item>
    )
}


const StockTableBetterOrWorse = () => {
    const [stocksBetterList, setStocksBetterList] = useState([]);
    const [stocksWorseList, setStocksWorseList] = useState([]);

    useEffect(() => {
        //Stocks para la tabla de Mejores Acciones del dia
        fetch("api/Home/GetStocksOrderByChangePercent?take=10&order=DESC")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStocksBetterList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
        //Stocks para la tabla de las peores acciones del dia
        fetch("api/Home/GetStocksOrderByChangePercent?take=10&order=ASC")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStocksWorseList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });


    }, []);

    return (
        <>
            <ListGroup>
                <ListGroup.Item variant="success"><h4>Lo Mejor</h4></ListGroup.Item>
                {
                    stocksBetterList.map(q => stockItem(q))
                }
            </ListGroup>
            <br />
            <ListGroup>
                <ListGroup.Item variant="danger"><h4>Lo Peor</h4></ListGroup.Item>
                {
                    stocksWorseList.map(q => stockItem(q))
                }
            </ListGroup>
        </>
    )
}

export default StockTableBetterOrWorse;