import React from "react";
import Styles from "./PlusIcon.css";
import AddIcon from "../../../images/add_icon.png";


const PlusIcon = ({ onClick, width, title }) => {
    return (<img className="PlusIcon" src={AddIcon} width={width} title={title} onClick={onClick} alt={title} />)
} 


export default PlusIcon;