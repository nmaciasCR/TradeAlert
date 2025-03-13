import React from "react";
import Styles from "./PortfolioTable.css";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../../../../components/Common/StockItem/StockItem";
import { Trunc2Decimal } from "../../../../Utils/Numbers";
import ActionsContainer from "../ActionsContainer/ActionsContainer.jsx";
import { Link } from 'react-router-dom';



const PortfolioTable = ({ portfolioStocks, refreshTablePortfolio }) => {

    return (
        <Table responsive className="portfolio-table">
            <thead>
                <tr>
                    <th>Simbolo</th>
                    <th>Mercado</th>
                    <th>Nombre</th>
                    <th>Prioridad</th>
                    <th>% Portf.</th>
                    <th>Cant.</th>
                    <th>Moneda</th>
                    <th>Precio</th>
                    <th>Var. %</th>
                    <th>Variación</th>
                    <th>Monto<br/>Total (€)</th>
                    <th>Precio Prom.<br />de Compra</th>
                    <th>Ganancia</th>
                    <th>Estocástico</th>
                    <th>Última<br />Revisión</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className="table-body">
                {portfolioStocks.map(q =>
                (<tr key={q.id}>
                    <td>
                        <Link to={`/Quote?q=${q.symbol}`} className="symbol-link">{StockItem.symbol(q.symbol)}</Link>
                    </td>
                    <td className="align-center">{StockItem.market(q._market.flag, q._market.description)}</td>
                    <td>{q.name} {StockItem.infoRedIcon(q.reviewRequired)}</td>
                    <td className="align-center">{StockItem.priorityIcon(q.priorityId)}</td>
                    <td className="align-right">{Trunc2Decimal(q._Portfolio.weightingPercent)}</td>
                    <td className="align-right">{q._Portfolio.quantity}</td>
                    <td className="align-center"><span title={q._currency.name}>{q._currency.code}</span></td>
                    <td className="align-right">{Trunc2Decimal(q.regularMarketPrice)}</td>
                    <td>{StockItem.number(Trunc2Decimal(q.regularMarketChangePercent), true)}</td>
                    <td>{StockItem.number(Trunc2Decimal(q.regularMarketChange), false)}</td>
                    <td className="align-right">{Trunc2Decimal(q._Portfolio.euroTotalAmount)}</td>
                    <td className="align-right">{Trunc2Decimal(q._Portfolio.averagePurchasePrice)}</td>
                    <td>{StockItem.profit(Trunc2Decimal(q._Portfolio.euroProfit), Trunc2Decimal(q._Portfolio.profitPercent))}</td>
                    <td className="align-right">{StockItem.stochastic(Trunc2Decimal(q.stochasticD))}</td>
                    <td>{StockItem.lastReview(q.dateReviewDaysDiff, q.dateReview)}</td>
                    <td><ActionsContainer portfolioQuote={q} refreshTable={refreshTablePortfolio} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );


}


export default PortfolioTable;