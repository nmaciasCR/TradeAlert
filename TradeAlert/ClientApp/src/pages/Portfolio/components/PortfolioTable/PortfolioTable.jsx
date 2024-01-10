import React from "react";
import Table from 'react-bootstrap/Table';
import { StockItem } from "../../../../components/Common/StockItem/StockItem";
import { Trunc2Decimal } from "../../../../Utils/Numbers";
import ActionsContainer from "../ActionsContainer/ActionsContainer.jsx";



const PortfolioTable = ({ portfolioStocks, refreshTablePortfolio }) => {

    return (
        <Table responsive>
            <thead>
                <tr>
                    <th>Simbolo</th>
                    <th>Mercado</th>
                    <th>Nombre</th>
                    <th>Prioridad</th>
                    <th>% Ponderación</th>
                    <th>Cantidad</th>
                    <th>Moneda</th>
                    <th>Precio</th>
                    <th>%</th>
                    <th>Variación</th>
                    <th>Monto Total (€)</th>
                    <th>Última Revisión</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {portfolioStocks.map(q =>
                (<tr key={q.quoteId}>
                    <td>{StockItem.symbol(q._quote.symbol)}</td>
                    <td>{StockItem.market(q._quote._market.flag, q._quote._market.description)}</td>
                    <td>{q._quote.name} {StockItem.infoRedIcon(q._quote.reviewRequired)}</td>
                    <td>{StockItem.priorityIcon(q._quote.priorityId)}</td>
                    <td>{Trunc2Decimal(q.weightingPercent)}</td>
                    <td>{q.quantity}</td>
                    <td>{q._quote._currency.code}</td>
                    <td>{Trunc2Decimal(q._quote.regularMarketPrice)}</td>
                    <td>{StockItem.changePercent(Trunc2Decimal(q._quote.regularMarketChangePercent))}</td>
                    <td>{StockItem.change(Trunc2Decimal(q._quote.regularMarketChange))}</td>
                    <td>{Trunc2Decimal(q.euroTotalAmount)}</td>
                    <td>{StockItem.lastReview(q._quote.dateReviewDaysDiff, q._quote.dateReview)}</td>
                    <td><ActionsContainer portfolioQuote={q} refreshTable={refreshTablePortfolio} /></td>
                </tr>)
                )}
            </tbody>
        </Table>
    );


}


export default PortfolioTable;