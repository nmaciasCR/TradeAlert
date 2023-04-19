import React from "react";

import Styles from "./RemoveIcon.css";
import garbageIcon from "../../../images/remove_icon.png";



const RemoveIcon = ({ width, title, onClick }) => {


    return (<img className="RemoveIcon" src={garbageIcon} width={width} title={title} onClick={onClick} />)


}


export default RemoveIcon;