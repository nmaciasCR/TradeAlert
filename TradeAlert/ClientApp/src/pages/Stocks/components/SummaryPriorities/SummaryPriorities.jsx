import React from "react";
import Styles from "./SummaryPriorities.css";
import highPriorityIcon from "../../../../images/high_priority_icon.png";
import mediumPriorityIcon from "../../../../images/medium_priority_icon.png";
import lowPriorityIcon from "../../../../images/low_priority_icon.png";
import infoIcon from "../../../../images/info_red_icon.png";




const Summary = ({ quotes }) => {
    const HIGH_PRIORITY = 1;
    const MEDIUM_PRIORIDAD = 2;
    const LOW_PRIORITY = 3;


    return (
        <div className="Summary-container">
            <div className="item">
                <img className="icon" src={highPriorityIcon} width="32" title="Alta Prioridad" />
                <span className="item-count">{quotes.filter(s => s.priorityId == HIGH_PRIORITY).length}</span>
            </div>
            <div className="item">
                <img className="icon" src={mediumPriorityIcon} width="32" title="Alta Prioridad" />
                <span className="item-count">{quotes.filter(s => s.priorityId == MEDIUM_PRIORIDAD).length}</span>
            </div>
            <div className="item">
                <img className="icon" src={lowPriorityIcon} width="32" title="Alta Prioridad" />
                <span className="item-count">{quotes.filter(s => s.priorityId == LOW_PRIORITY).length}</span>
            </div>
            <div className="item">
                <img className="icon" src={infoIcon} width="32" title="Revisión Requerida" />
                <span className="item-count">{quotes.filter(s => s.reviewRequired).length}</span>
            </div>
        </div>

    )
}

export default Summary;