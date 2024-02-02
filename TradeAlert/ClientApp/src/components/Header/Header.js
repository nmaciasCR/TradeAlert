import React, { useEffect, useState, useCallback } from "react";
import style from "./Header.css";
import logoChart from "../../images/tradeAlertLogo.png";
import { Link } from 'react-router-dom';
import homeIcon from "../../images/home_icon.png";
import stocksIcon from "../../images/stock_icon.png";
import portfolioIcon from "../../images/portfolio_icon.png";
import NotificationIcon from "./NotificationIcon.jsx";

const Header = () => {

    const [headerData, setHeaderData] = useState([]);


    const loadHeaderData = useCallback(() => {
        fetch("api/Header/GetData")
            .then(response => { return response.json() })
            .then(responseJson => {
                setHeaderData(responseJson);
            })
            .catch(error => {
                console.log(error);
            })
    }, []);


    useEffect(() => {
        loadHeaderData();
    }, [loadHeaderData]);




    return (
        <div className="header">
            <div className="logoContainer">
                <div className="logoTittle">TradeAlert</div>
                <img src={logoChart} height="80px" alt="TradeAlert" />
            </div>
            <div className="iconContainer">
                <Link to="/Home">
                    <img src={homeIcon} height="60px" title="Inicio" alt="Inicio" />
                </Link>
                <Link to="/Stocks">
                    <img src={stocksIcon} height="60px" title="Cotizaciones" alt="Cotizaciones" />
                </Link>
                <Link to="/Portfolio">
                    <img src={portfolioIcon} height="60px" title="Portafolio" alt="Portafolio" />
                </Link>
                <NotificationIcon notifications={headerData.notification} />
            </div>
        </div>
    )

}

export default Header;