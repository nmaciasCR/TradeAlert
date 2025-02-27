import React from "react";
import Styles from "./ActionsContainer.css";
import ActionAlert from "../../../../components/QuoteBell/QuoteBell";
import EditIcon from "../EditStockIcon/EditStockIcon.jsx";
import DeleteStockIcon from "../DeleteStockIcon/DeleteStockIcon.jsx";



const ActionsContainer = ({ portfolioQuote, refreshTable }) => {



    return (
        <div className="container">
            <div>
                <ActionAlert quote={portfolioQuote} afterCloseModal={refreshTable} />
            </div>
            <div>
                <EditIcon portfolioQuote={portfolioQuote} refreshTable={refreshTable} />
            </div>
            <div>
                <DeleteStockIcon quoteId={portfolioQuote.id} name={portfolioQuote.name} refreshTable={refreshTable} />
            </div>
        </div>
    )

}


export default ActionsContainer;