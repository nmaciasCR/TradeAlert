import React from "react";
import Styles from "./StocksTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../StockItem/StockItem";
import ActionContainer from "../ActionsContainer/ActionsContainer";



const StockYable = (props) => {

    return (
        <Table responsive>
            <thead>
                <tr>
                    <th>Simbolo</th>
                    <th>Nombre</th>
                    <th>Prioridad</th>
                    <th>Moneda</th>
                    <th>Precio</th>
                    <th>%</th>
                    <th>Variación</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {props.quotes.map(q => 
                (<tr>
                    <td>{StockItem.symbol(q.symbol)}</td>
                    <td>{q.name}</td>
                    <td>{q.priorityId}</td>
                    <td>{q.currency}</td>
                    <td>{q.regularMarketPrice}</td>
                    <td>{StockItem.changePercent(q.regularMarketChangePercent)}</td>
                    <td>{StockItem.change(q.regularMarketChange)}</td>
                    <td><ActionContainer quote={q} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );

}


export default StockYable;