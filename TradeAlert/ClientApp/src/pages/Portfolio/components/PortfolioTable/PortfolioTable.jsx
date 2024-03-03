import React from "react";
import Styles from "./PortfolioTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../../../../components/Common/StockItem/StockItem";
import { Trunc2Decimal } from "../../../../Utils/Numbers";
import ActionsContainer from "../ActionsContainer/ActionsContainer.jsx";



const PortfolioTable = ({ portfolioStocks, refreshTablePortfolio }) => {

    return (
        <Table responsive className="portfolio-table">
            <thead>
                <tr>
                    <th>Simbolo</th>
                    <th>Mercado</th>
                    <th>Nombre</th>
                    <th>Prioridad</th>
                    <th>Ponderación %</th>
                    <th>Cant.</th>
                    <th>Moneda</th>
                    <th>Precio</th>
                    <th>Var. %</th>
                    <th>Variación</th>
                    <th>Monto<br/>Total (€)</th>
                    <th>Precio Prom.<br />de Compra</th>
                    <th>Ganancia</th>
                    <th>Última<br />Revisión</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className="table-body">
                {portfolioStocks.map(q =>
                (<tr key={q.quoteId}>
                    <td>{StockItem.symbol(q._quote.symbol)}</td>
                    <td className="align-center">{StockItem.market(q._quote._market.flag, q._quote._market.description)}</td>
                    <td>{q._quote.name} {StockItem.infoRedIcon(q._quote.reviewRequired)}</td>
                    <td className="align-center">{StockItem.priorityIcon(q._quote.priorityId)}</td>
                    <td className="align-right">{Trunc2Decimal(q.weightingPercent)}</td>
                    <td className="align-right">{q.quantity}</td>
                    <td className="align-center"><span title={q._quote._currency.name}>{q._quote._currency.code}</span></td>
                    <td className="align-right">{Trunc2Decimal(q._quote.regularMarketPrice)}</td>
                    <td>{StockItem.number(Trunc2Decimal(q._quote.regularMarketChangePercent), true)}</td>
                    <td>{StockItem.number(Trunc2Decimal(q._quote.regularMarketChange), false)}</td>
                    <td className="align-right">{Trunc2Decimal(q.euroTotalAmount)}</td>
                    <td className="align-right">{Trunc2Decimal(q.averagePurchasePrice)}</td>
                    <td>{StockItem.profit(Trunc2Decimal(q.euroProfit), Trunc2Decimal(q.profitPercent))}</td>
                    <td>{StockItem.lastReview(q._quote.dateReviewDaysDiff, q._quote.dateReview)}</td>
                    <td><ActionsContainer portfolioQuote={q} refreshTable={refreshTablePortfolio} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );


}


export default PortfolioTable;