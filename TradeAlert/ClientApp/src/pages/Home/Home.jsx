import React, { useEffect, useState } from "react";
import Header from "../../components/Header/Header";
import QuotesGrid from "../../components/QuotesGrid/QuotesGrid";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Container from 'react-bootstrap/Container';
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
                        <h2>Alta Prioridad</h2>
                        <QuotesGrid quotes={stocksList.filter(s => s.priorityId == 1)} />

                    </Col>
                    <Col sm={2}>
                        <ListGroup>
                            <ListGroup.Item variant="success">Lo Mejor</ListGroup.Item>
                            {
                                stocksBetterList.map(q => (<ListGroupItem quote={q} />))
                            }
                        </ListGroup>
                        <br />
                        <ListGroup>
                            <ListGroup.Item variant="danger">Lo Peor</ListGroup.Item>
                            {
                                stocksWorseList.map(q => (<ListGroupItem quote={q} />))
                            }
                        </ListGroup>

                    </Col>
                </Row>
            </div>


        </div>
    )
}

export default Home;