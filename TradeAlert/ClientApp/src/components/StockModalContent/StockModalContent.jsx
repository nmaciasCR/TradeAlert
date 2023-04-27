import React, { useEffect, useState, useCallback } from "react";
import styles from "./StockModalContent.css";
import ListGroup from 'react-bootstrap/ListGroup';
import PlusIcon from "../Common/PlusIcon/PlusIcon";
import AcceptIcon from "../Common/AcceptIcon/AcceptIcon";
import CancelIcon from "../Common/CancelIcon/CancelIcon";
import RemoveIcon from "../Common/RemoveIcon/RemoveIcon";


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
    const cssname = containerClass(props.quote.regularMarketChangePercent); //verde o rojo
    const [supportsAlertsList, setSupportsAlertsList] = useState([]); //listado de soportes
    const [resistorsAlertsList, setResistorsAlertsList] = useState([]); //listado de resistencias

    const [showResistorsInput, setShowResistorsInput] = useState(false); //Se muestra el input de resistencias?
    const [showSupportInput, setShowSupportInput] = useState(false); //Se muestra el input de soportes?


    const LoadQuotesAlerts = useCallback(() => {
        fetch(`api/Stocks/GetQuotesAlerts?stockId=${props.quote.id}`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la lista de soportes ordenadas de manor a mayor
                setSupportsAlertsList(responseJson.filter(s => s.quoteAlertTypeId == 1));
                setResistorsAlertsList(responseJson.filter(s => s.quoteAlertTypeId == 2));
            })
            .catch(error => {
                console.log(error);
            });
    }, []);

    useEffect(() => {
        LoadQuotesAlerts();
    }, [LoadQuotesAlerts]);

    const AcceptIconOnClick = (quoteId, alertType, elem) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                'QuoteId': quoteId,
                'TypeId': alertType,
                'Price': document.getElementById(elem).value
            })
        };

        fetch('api/Stocks/AddQuoteAlert', requestOptions)
            .then(response => response.json())
            .then(data => {
                LoadQuotesAlerts();
                setShowResistorsInput(false);
                setShowSupportInput(false);
                document.getElementById(elem).value = "";
            }
            );

    }


    const RemoveIconOnClick = (quoteId, alertId) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                'QuoteId': quoteId,
                'AlertId': alertId,
            })
        };

        fetch('api/Stocks/DeleteQuoteAlert', requestOptions)
            .then(data => {
                LoadQuotesAlerts();
            }
            );
    }


    const PlusResistorIconClick = () => {
        setShowResistorsInput(true);
    }
    const PlusSupportIconClick = () => {
        setShowSupportInput(true);
    }

    const CancelResistorIconOnClick = () => {
        setShowResistorsInput(false);
    }

    const CancelSupportIconOnClick = () => {
        setShowSupportInput(false);
    }


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
                <ListGroup.Item variant="success">Resistencias <PlusIcon onClick={PlusResistorIconClick} width="25" title="Agregar Resistencia" /></ListGroup.Item>
                <ListGroup.Item className={`itemAlert ${showResistorsInput ? "show-element" : "hide-element"}`}>
                    <input type="text" id="txtResistorAlertPrice" className="inputNewAlert" />
                    <AcceptIcon onClick={() => AcceptIconOnClick(props.quote.id, 2, "txtResistorAlertPrice")} width="25" title="Agregar" />
                    <CancelIcon onClick={CancelResistorIconOnClick} width="25" title="Cancelar" />
                </ListGroup.Item>
                {resistorsAlertsList.length > 0 ? (
                    /*Listamos las resistencias de mayor a menor*/
                    resistorsAlertsList.sort((a, b) => a.price < b.price ? 1: -1).map(r => (
                        <ListGroup.Item className="itemAlert">
                            {r.price} ({r.regularMarketPercentDiff} %)
                            <span className="remove-icon"><RemoveIcon width="25" title="Eliminar" onClick={() => RemoveIconOnClick(props.quote.id, r.id)} /></span>
                        </ListGroup.Item>)
                    )
                ) : (<ListGroup.Item>No hay resistencias</ListGroup.Item>)
                }
            </ListGroup>

            <div className="quotes">
                <span className="price">{props.quote.regularMarketPrice}</span>
            </div>

            <ListGroup className="listGroupAlerts">
                <ListGroup.Item variant="danger">Soportes <PlusIcon onClick={PlusSupportIconClick} width="25" title="Agregar Soporte" /></ListGroup.Item>
                <ListGroup.Item className={`itemAlert ${showSupportInput ? "show-element" : "hide-element"}`}>
                    <input type="text" id="txtSupportAlertPrice" className="inputNewAlert" />
                    <AcceptIcon onClick={() => AcceptIconOnClick(props.quote.id, 1, "txtSupportAlertPrice")} width="25" title="Agregar" />
                    <CancelIcon onClick={CancelSupportIconOnClick} width="25" title="Cancelar" />
                </ListGroup.Item>
                {supportsAlertsList.length > 0 ? (
                    supportsAlertsList.sort((a, b) => a.price < b.price ? 1 : -1).map(r => (
                        <ListGroup.Item className="itemAlert">
                            {r.price} ({r.regularMarketPercentDiff} %)
                            <span className="remove-icon"><RemoveIcon width="25" title="Eliminar" onClick={() => RemoveIconOnClick(props.quote.id, r.id)} /></span>
                        </ListGroup.Item>)
                    )
                ) : (<ListGroup.Item>No hay soportes</ListGroup.Item>)
                }
            </ListGroup>

        </div>
    )

}


export default StockModalContent;