import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Portfolio.css";
import Header from "../../components/Header/Header";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import ListGroup from 'react-bootstrap/ListGroup';
import PortfolioTable from "./components/PortfolioTable/PortfolioTable.jsx";




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
                        <PortfolioTable portfolioStocks={portfolioStocks} />


                    </Col>
                    <Col sm={2}>
                        <ListGroup>
                            <ListGroup.Item variant="success"><h4>Lo Mejor</h4></ListGroup.Item>
                            {
                                //stocksBetterList.map(q => (<ListGroupItem key={q.id} quote={q} />))
                            }
                        </ListGroup>
                        <br />
                        <ListGroup>
                            <ListGroup.Item variant="danger"><h4>Lo Peor</h4></ListGroup.Item>
                            {
                                //stocksWorseList.map(q => (<ListGroupItem key={q.id} quote={q} />))
                            }
                        </ListGroup>

                    </Col>
                </Row>
            </div>


        </div>


    )

}


export default Portfolio;