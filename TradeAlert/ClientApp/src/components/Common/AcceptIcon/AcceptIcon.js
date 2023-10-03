import React from "react";
import Styles from "./AcceptIcon.css";
import Accept_Icon from "../../../images/accept_Icon.png";


const AcceptIcon = ({ onClick, width, title }) => {
    return (<img className="AcceptIcon" src={Accept_Icon} width={width} title={title} onClick={onClick} alt={title} />)
}


export default AcceptIcon;