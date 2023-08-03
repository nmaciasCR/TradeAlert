import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Stocks.css";
import Header from "../../components/Header/Header";
import StocksTable from "./components/StocksTable/StocksTable";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Summary from "./components/SummaryPriorities/SummaryPriorities";




const Stocks = () => {
    const [stocksList, setStockList] = useState([]);
    const HIGH_PRIORITY = 1;
    const MEDIUM_PRIORIDAD = 2;
    const LOW_PRIORITY = 3;

    const loadTableStocks = useCallback(() => {
        fetch("api/Stocks/GetStocks")
            .then(response => { return response.json() })
            .then(responseJson => {
                //rdenamos las acciones por fecha de revision
                //Primeros las revisadas hace mas tiempo
                let sortedList = responseJson.sort((a, b) => new Date(a.dateReview) > new Date(b.dateReview) ? 1 : -1)
                setStockList(sortedList);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        loadTableStocks();
    }, [loadTableStocks]);

    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="Container">
                <div className="Title-container">
                    <div className="Title">Listado de Acciones ({stocksList.length})</div>
                    <Summary quotes={stocksList} />
                </div>
                <br />
                <StocksTable quotes={stocksList} refreshTableStocks={loadTableStocks} />
            </div>
        </div>
    )
}


export default Stocks;