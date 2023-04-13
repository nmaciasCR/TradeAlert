import React from "react";
import style from "./Header.css";
import logoChart from "../../images/tradeAlertLogo.png";


const Header = () => {

    return (
        <div className="header">
            <div className="logoContainer">
                <div className="logoTittle">TradeAlert</div>
                <img src={logoChart} height="80px" />
            </div>

        </div>
    )

}

export default Header;