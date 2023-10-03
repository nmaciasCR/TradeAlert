import React, { useEffect, useState } from "react";
import Styles from "./Home.css";
import Header from "../../components/Header/Header";
import QuotesGrid from "../../components/QuotesGrid/QuotesGrid";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import ListGroup from 'react-bootstrap/ListGroup';
import ListGroupItem from './components/ListGroupItemBetterOrWorse/ListGroupItemBetterOrWorse.jsx';



const Home = () => {
    const [stocksList, setStockList] = useState([]);
    const [stocksBetterList, setStocksBetterList] = useState([]);
    const [stocksWorseList, setStocksWorseList] = useState([]);

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
        //Stocks para la tabla de Mejores Acciones del dia
        fetch("api/Home/GetStocksOrderByChangePercent?take=8&order=DESC")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStocksBetterList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
        //Stocks para la tabla de las peores acciones del dia
        fetch("api/Home/GetStocksOrderByChangePercent?take=8&order=ASC")
            .then(response => { return response.json() })
            .then(responseJson => {
                setStocksWorseList(responseJson);
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
                    <Col sm={10}>
                        <ListGroup>
                            <ListGroup.Item variant="secondary"><h3>Alta Prioridad</h3></ListGroup.Item>
                            <ListGroup.Item className="mainItemListGroup">
                                <QuotesGrid quotes={stocksList.filter(s => s.priorityId === 1)} />
                            </ListGroup.Item>
                        </ListGroup>
                    </Col>
                    <Col sm={2}>
                        <ListGroup>
                            <ListGroup.Item variant="success"><h3>Lo Mejor</h3></ListGroup.Item>
                            {
                                stocksBetterList.map(q => (<ListGroupItem key={q.id} quote={q} />))
                            }
                        </ListGroup>
                        <br />
                        <ListGroup>
                            <ListGroup.Item variant="danger"><h3>Lo Peor</h3></ListGroup.Item>
                            {
                                stocksWorseList.map(q => (<ListGroupItem key={q.id} quote={q} />))
                            }
                        </ListGroup>

                    </Col>
                </Row>
            </div>


        </div>
    )
}

export default Home;