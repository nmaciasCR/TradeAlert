import React from "react";
import Styles from "./StocksTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../StockItem/StockItem";
import ActionContainer from "../ActionsContainer/ActionsContainer";
import { format } from 'date-fns';



const StockTable = (props) => {

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
                    <th>Última Revisión</th>
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
                    <td>{format(new Date(q.dateReview), 'dd/MM/yyyy')}</td>
                    <td><ActionContainer quote={q} refreshTable={props.refreshTableStocks} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );

}


export default StockTable;