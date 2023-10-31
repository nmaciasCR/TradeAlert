import React from "react";
import style from "./Header.css";
import logoChart from "../../images/tradeAlertLogo.png";
import { Link } from 'react-router-dom';
import homeIcon from "../../images/home_icon.png";
import stocksIcon from "../../images/stock_icon.png";
import notificationIcon from "../../images/bell_icon.png";
import portfolioIcon from "../../images/portfolio_icon.png";

const Header = () => {

    return (
        <div className="header">
            <div className="logoContainer">
                <div className="logoTittle">TradeAlert</div>
                <img src={logoChart} height="80px" alt="TradeAlert"/>
            </div>
            <div className="iconContainer">
                <Link to="/Home">
                    <img src={homeIcon} height="60px" title="Inicio" className="iconActions" alt="Inicio" />
                </Link>
                <Link to="/Stocks">
                    <img src={stocksIcon} height="60px" title="Cotizaciones" className="iconActions" alt="Cotizaciones" />
                </Link>
                <Link to="/Portfolio">
                    <img src={portfolioIcon} height="60px" title="Portafolio" className="iconActions" alt="Portafolio" />
                </Link>
                <Link to="/">
                    <img src={notificationIcon} height="60px" title="Notificaciones" className="iconActions" alt="Notificaciones" />
                </Link>

            </div>
        </div>
    )

}

export default Header;