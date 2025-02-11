import React, { useEffect, useState, useCallback } from "react";
import Styles from "./MarketFilter.css";
import ListGroup from 'react-bootstrap/ListGroup';
import Form from 'react-bootstrap/Form';


const MarketFilter = ({ stockFiltersList, fnMarketFilters }) => {

    //Todos los grupos de la DDBB
    const [marketsList, setMarketsList] = useState([]);


    //Obtenemos todos los grupos de la DDBB
    const LoadMarketsList = useCallback(() => {
        fetch(`api/Market/GetList`)
            .then(response => { return response.json() })
            .then(responseJson => {
                //Guardamos la LISTA DE MERCADOS
                setMarketsList(responseJson);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);


    useEffect(() => {
        //Obtenemos el listado de grupos
        LoadMarketsList();
    }, [LoadMarketsList]);


    const CreateMarketLabel = (desc, qty) => {
        return `${desc} (${qty})`;
    }


    const CreateMarketListItem = (market) => {

        return (
            <ListGroup.Item key={market.id}>
                <div className="">
                    <Form.Check
                        type="checkbox"
                        id={market.id}
                        label={CreateMarketLabel(market.description, market.quotesQty)}
                        checked={stockFiltersList.markets.some(m => m === market.id)}
                        onChange={(event) => fnMarketFilters(event, market.id)}
                    />
                </div>
            </ListGroup.Item>

        )
    }




    return (
        <ListGroup className="market-filter-container">
            <ListGroup.Item className="title">Seleccione mercados para filtrar</ListGroup.Item>
            {
                marketsList.map(m => (CreateMarketListItem(m)))
            }
        </ListGroup>
    )
}


export default MarketFilter;