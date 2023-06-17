import React, { useEffect, useState } from "react";
import Header from "../../components/Header/Header";
import QuotesGrid from "../../components/QuotesGrid/QuotesGrid";
import IndexMarket from "../../components/IndexMarket/IndexMarket";

const Home = () => {
    const [stocksList, setStockList] = useState([]);

    useEffect(() => {
        fetch("api/Home/GetStocksOrderAlerts?priorityId=1")
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
            <IndexMarket />
            <div className="Container" style={{ padding: "10px" }}>
                <h2>Alta Prioridad</h2>
                <QuotesGrid quotes={stocksList.filter(s => s.priorityId == 1)} />
            </div>
        </div>
    )
}

export default Home;