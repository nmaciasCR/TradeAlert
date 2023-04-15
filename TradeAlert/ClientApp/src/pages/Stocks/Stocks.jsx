import React, { useEffect, useState } from "react";
import Styles from "./Stocks.css";
import Header from "../../components/Header/Header";
import StocksTable from "./components/StocksTable/StocksTable"; 




const Stocks = () => {


    const [stocksList, setStockList] = useState([]);

    useEffect(() => {
        fetch("api/Stocks/GetStocks")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStockList(responseJson);
            })
            .catch(error => {
                console.log(error);
            })
    }, []);




    return (
        <div>
            <Header />
            <div className="Container">
                <h1>Listado de Acciones</h1>
                <StocksTable quotes={stocksList} />

            </div>
        </div>
    )
}


export default Stocks;