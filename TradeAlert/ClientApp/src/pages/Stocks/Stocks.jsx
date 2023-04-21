import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Stocks.css";
import Header from "../../components/Header/Header";
import StocksTable from "./components/StocksTable/StocksTable";




const Stocks = () => {
    const [stocksList, setStockList] = useState([]);

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
            <div className="Container">
                <h1>Listado de Acciones</h1>
                <StocksTable quotes={stocksList} refreshTableStocks={loadTableStocks} />
            </div>
        </div>
    )
}


export default Stocks;