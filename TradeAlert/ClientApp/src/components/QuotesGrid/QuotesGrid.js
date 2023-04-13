import React from "react";
import styles from "./QuotesGrid.css";
import QuoteBlock from "../QuoteBlock/QuoteBlock";



const Grid = (props) => {

    return (
        <div className="quotesGrid">
            {props.quotes.map(s => (
                <QuoteBlock quote={s} />
            ))}


        </div>
    )

}

export default Grid;