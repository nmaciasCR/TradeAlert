import React from "react";
import style from "./Header.css";
import logoChart from "../../images/tradeAlertLogo.png";
import { Link } from 'react-router-dom';


const Header = () => {

    return (
        <div className="header">
            <div className="logoContainer">
                <div className="logoTittle">TradeAlert</div>
                <img src={logoChart} height="80px" />
            </div>
            <div>
                <Link to="/Stocks">Acciones</Link>
            </div>
        </div>
    )

}

export default Header;