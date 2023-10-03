import React from "react";
import Styles from "./CancelIcon.css";
import Cancel_Icon from "../../../images/cancel_Icon.png";


const CancelIcon = ({ onClick, width, title }) => {
    return (<img className="CancelIcon" src={Cancel_Icon} width={width} title={title} onClick={onClick} alt={title} />)
}


export default CancelIcon;