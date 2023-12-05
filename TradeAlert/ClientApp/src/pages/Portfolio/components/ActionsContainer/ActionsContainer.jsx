import React from "react";
import Styles from "./ActionsContainer.css";
import ActionAlert from "../../../../components/QuoteBell/QuoteBell";
import EditIcon from "../EditStockIcon/EditStockIcon.jsx";
import DeleteStockIcon from "../DeleteStockIcon/DeleteStockIcon.jsx";



const ActionsContainer = ({ portfolioQuote, refreshTable }) => {



    return (
        <div className="container">
            <div>
                <ActionAlert quote={portfolioQuote._quote} afterCloseModal={refreshTable} />
            </div>
            <div>
                <EditIcon portfolioQuote={portfolioQuote} refreshTable={refreshTable} />
            </div>
            <div>
                <DeleteStockIcon quoteId={portfolioQuote.quoteId} name={portfolioQuote._quote.name} refreshTable={refreshTable} />
            </div>
        </div>
    )

}


export default ActionsContainer;