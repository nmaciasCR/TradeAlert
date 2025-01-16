import React, { useEffect, useState, useCallback } from "react";
import Styles from './Quote.css';
import Header from "../../components/Header/Header";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import PortfolioTable from '../../components/PortfolioTable/PortfolioTable.jsx';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useLocation } from 'react-router-dom';
import QuoteHeader from './components/QuoteHeader/QuoteHeader';
import PortfolioIndicator from './components/PortfolioIndicator/PortfolioIndicator';
import Alerts from './components/Alerts/Alerts';


const Quote = () => {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const q = queryParams.get('q');

    //Objeto Quote
    const [quote, setQuote] = useState(null);

    const LoadQuote = useCallback(() => {
        fetch(`api/Quote/Get?q=${q}`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la quote
                setQuote(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);

    useEffect(() => {
        LoadQuote();
    }, [LoadQuote]);





    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="container-fluid">
                <Row>
                    <Col sm={2}>
                        <PortfolioTable />
                    </Col>
                    <Col sm={10}>
                        <br />
                        <div className="quote">
                            {/*Encabezado de una accion*/}
                            {quote && <QuoteHeader quote={quote} fnLoadQuote={LoadQuote} />}
                            {/*Indicador de Portfolio*/}
                            {quote?._Portfolio && <PortfolioIndicator
                                quantity={quote._Portfolio.quantity}
                                currency={quote._currency.code}
                                avgPrice={quote._Portfolio.averagePurchasePrice} />
                            }
                            <br /><br />
                            <row className="content-row">
                                <Col sm={6}>
                                    {/*Alertas*/}
                                    {quote && <Alerts quote={quote} />}
                                </Col>
                                <Col sm={6}>

                                </Col>

                            </row>



                        </div>






                    </Col>
                </Row>

            </div>
        </div>
    )


}

export default Quote;