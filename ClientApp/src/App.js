import React, { useEffect, useState } from "react";
import Header from "./components/Header/Header";


const App = () => {

    const [stocksList, setStockList] = useState([]);

    useEffect(() => {
        fetch("api/Stocks/GetStocks")
            .then(response => { return response.json() })
            .then(responseJson => {

                setStockList(responseJson);
            });
    }, []);





    return (
        <div>
            <Header />
            <div className="Container">
                <h2>Stocks</h2>
                <div className="row">
                    <div className="col-sm-12">
                        <table className="table table-striped">
                            <thead>
                                <tr>
                                    <th>Simbolo</th>
                                    <th>Nombre</th>
                                    <th>Moneda</th>
                                    <th>Precio</th>
                                    <th>Variación</th>
                                    <th>Prioridad</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    stocksList.map((item) => (
                                        <tr>
                                            <td>{item.symbol}</td>
                                            <td>{item.name}</td>
                                            <td>{item.currency}</td>
                                            <td>{item.regularMarketPrice}</td>
                                            <td>{item.regularMarketChangePercent}</td>
                                            <td>{item.priorityId}</td>
                                        </tr>

                                    ))
                                }

                            </tbody>

                        </table>
                    </div>
                </div>
            </div>

        </div>
    )

}


export default App;