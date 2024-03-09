import React from "react";
import Styles from "./StocksTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../../../../components/Common/StockItem/StockItem";
import ActionContainer from "../ActionsContainer/ActionsContainer";
import { Trunc2Decimal } from "../../../../Utils/Numbers.js";


const StockTable = (props) => {

    return (
        <Table responsive className="stockTable-table">
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
            <tbody className="table-body">
                {props.quotes.map(q => 
                (<tr key={q.id}>
                    <td>{StockItem.symbol(q.symbol)}</td>
                    <td className="align-center">{StockItem.market(q._market.flag, q._market.description)}</td>
                    <td>{q.name} {StockItem.infoRedIcon(q.reviewRequired)} {StockItem.portfolioIcon(q._Portfolio !== null)}</td>
                    <td className="align-center">{StockItem.priorityIcon(q.priorityId)}</td>
                    <td className="align-center"><span title={q._currency.name}>{q._currency.code}</span></td>
                    <td className="align-right">{Trunc2Decimal(q.regularMarketPrice)}</td>
                    <td className="align-right">{StockItem.number(Trunc2Decimal(q.regularMarketChangePercent), true)}</td>
                    <td className="align-right">{StockItem.number(Trunc2Decimal(q.regularMarketChange), false)}</td>
                    <td>{StockItem.lastReview(q.dateReviewDaysDiff, q.dateReview)}</td>
                    <td><ActionContainer quote={q} refreshTable={props.refreshTableStocks} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );

}


export default StockTable;