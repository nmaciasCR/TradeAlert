import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Portfolio.css";
import Header from "../../components/Header/Header";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import PortfolioTable from "./components/PortfolioTable/PortfolioTable.jsx";
import StockTableBetterOrWorse from '../../components/StockTableBetterOrWorse/StockTableBetterOrWorse.jsx';


const Portfolio = () => {

    const [portfolioStocks, setPortfolioStocks] = useState([]);

    const loadTablePortfolioStocks = useCallback(() => {
        fetch("api/Portfolio/GetPortfolio")
            .then(response => { return response.json() })
            .then(responseJson => {
                setPortfolioStocks(responseJson.sort((a, b) => a._quote.name > b._quote.name ? 1 : -1));
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        loadTablePortfolioStocks();
    }, [loadTablePortfolioStocks]);

    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="container-fluid container-portfolio">
                <Row>
                    <Col sm={10}>
                        <div className="title">Portfolio</div>
                        <PortfolioTable portfolioStocks={portfolioStocks} refreshTablePortfolio={loadTablePortfolioStocks} />
                    </Col>
                    <Col sm={2}>
                        <StockTableBetterOrWorse />
                    </Col>
                </Row>
            </div>
        </div>
    )

}


export default Portfolio;