import React, { useEffect, useState } from "react";
import Header from "./components/Header/Header";
import QuotesGrid from "./components/QuotesGrid/QuotesGrid";


const App = () => {

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
            <div className="Container" style={{padding: "10px"}}>
                <h2>Alta Prioridad</h2>
                <QuotesGrid quotes={stocksList.filter(s => s.priorityId == 1)} />

            </div>

        </div>
    )

}


export default App;