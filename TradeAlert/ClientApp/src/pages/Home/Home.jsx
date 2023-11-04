import React, { useEffect, useState } from "react";
import Styles from "./Home.css";
import Header from "../../components/Header/Header";
import QuotesGrid from "../../components/QuotesGrid/QuotesGrid";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import ListGroup from 'react-bootstrap/ListGroup';
import PortfolioTable from '../../components/Common/PortfolioTable/PortfolioTable.jsx';
import StockTableBetterOrWorse from '../../components/StockTableBetterOrWorse/StockTableBetterOrWorse.jsx';


const Home = () => {
    const [stocksList, setStockList] = useState([]);

    useEffect(() => {
        //Stocks para la grilla principal
        fetch("api/Home/GetStocksOrderAlerts?priorityId=1")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStockList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);


    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="container-fluid">
                <Row>
                    <Col sm={2}>
                        <PortfolioTable />
                    </Col>
                    <Col sm={8}>
                        <ListGroup>
                            <ListGroup.Item variant="secondary"><h4>Alta Prioridad</h4></ListGroup.Item>
                            <ListGroup.Item className="mainItemListGroup">
                                <QuotesGrid quotes={stocksList.filter(s => s.priorityId === 1)} />
                            </ListGroup.Item>
                        </ListGroup>
                    </Col>
                    <Col sm={2}>
                        <StockTableBetterOrWorse />
                    </Col>
                </Row>
            </div>
        </div>
    )
}

export default Home;