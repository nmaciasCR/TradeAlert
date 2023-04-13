import React, { useEffect, useState } from "react";
import styles from "./StockModalContent.css";
import ListGroup from 'react-bootstrap/ListGroup';


function containerClass(price) {
    switch (Math.sign(price)) {
        case 1:
            return "changePercent-up";
        case -1:
            return "changePercent-down";
        default:
            return "changePercent-zero";
    }
}


function StockModalContent(props) {
    const cssname = containerClass(props.quote.regularMarketChangePercent);
    const [supportsAlertsList, setSupportsAlertsList] = useState([]);
    const [resistorsAlertsList, setResistorsAlertsList] = useState([]);

    useEffect(() => {
        fetch(`api/Stocks/GetQuotesAlerts?stockId=${props.quote.id}`)
            .then(response => { return response.json() })
            .then(responseJson => {
                setSupportsAlertsList(responseJson.filter(s => s.quoteAlertTypeId == 1));
                setResistorsAlertsList(responseJson.filter(s => s.quoteAlertTypeId == 2));
            })
            .catch(error => {
                console.log(error);
            });
    }, []);


    return (
        <div className="stockModalContent">
            <div className="title">
                <div className="symbol">{props.quote.symbol}</div>
                <div className={`changePercent ${cssname}`}>
                    <div className="quote-arrow"></div>
                    {props.quote.regularMarketChangePercent} % ({props.quote.regularMarketChange})
                </div>
            </div>

            <div className="name">{props.quote.name}</div>
            <ListGroup className="listGroupAlerts">
                <ListGroup.Item variant="success">Resistencias</ListGroup.Item>
                {resistorsAlertsList.length > 0 ? (
                    resistorsAlertsList.map(r => (<ListGroup.Item className="itemAlert">{r.price}</ListGroup.Item>))
                ) : (<ListGroup.Item>No hay resistencias</ListGroup.Item>)
                }
            </ListGroup>

            <div className="quotes">
                <span className="price">{props.quote.regularMarketPrice}</span>
            </div>

            <ListGroup className="listGroupAlerts">
                <ListGroup.Item variant="danger">Soportes</ListGroup.Item>
                {supportsAlertsList.length > 0 ? (
                    supportsAlertsList.map(r => (<ListGroup.Item className="itemAlert">{r.price}</ListGroup.Item>))
                ) : (<ListGroup.Item>No hay soportes</ListGroup.Item>)
                }
            </ListGroup>

        </div>
    )

}


export default StockModalContent;