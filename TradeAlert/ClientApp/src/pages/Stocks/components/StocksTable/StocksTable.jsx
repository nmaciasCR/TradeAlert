import React from "react";
import Styles from "./StocksTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../StockItem/StockItem";
import ActionContainer from "../ActionsContainer/ActionsContainer";


const StockTable = (props) => {

    return (
        <Table responsive>
            <thead>
                <tr>
                    <th>Simbolo</th>
                    <th>Mercado</th>
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
                (<tr key={q.id}>
                    <td>{StockItem.symbol(q.symbol)}</td>
                    <td>{StockItem.market(q._market.flag, q._market.description)}</td>
                    <td>{q.name} {StockItem.infoRedIcon(q.reviewRequired)}</td>
                    <td>{StockItem.priorityIcon(q.priorityId)}</td>
                    <td>{q.currency}</td>
                    <td>{q.regularMarketPrice}</td>
                    <td>{StockItem.changePercent(q.regularMarketChangePercent)}</td>
                    <td>{StockItem.change(q.regularMarketChange)}</td>
                    <td>{StockItem.lastReview(q.dateReviewDaysDiff, q.dateReview)}</td>
                    <td><ActionContainer quote={q} refreshTable={props.refreshTableStocks} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );

}


export default StockTable;