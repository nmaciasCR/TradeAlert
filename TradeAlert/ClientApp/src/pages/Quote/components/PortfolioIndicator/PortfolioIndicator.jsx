import React from 'react';
import styles from "./PortfolioIndicator.css";
import portfolioIcon from "../../../../images/portfolio_brown_icon.png";
import { Trunc2Decimal } from '../../../../Utils/Numbers';





const PortfolioIndicator = ({ quantity, currency, avgPrice }) => {



    return (
        <div className="portfolio-indicator-container">
            <img className="" src={portfolioIcon} width="25px" height="25px" />
            <div className="description">
                <span className="quantity">{quantity}</span>
                <span> compradas a </span>
                <span className="avgPrice">{currency} {Trunc2Decimal(avgPrice)}</span>
            </div>

        </div>
    );
}



export default PortfolioIndicator;