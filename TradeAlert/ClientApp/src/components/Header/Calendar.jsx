import React from "react";
import Styles from "./Calendar.css";
import { Link } from 'react-router-dom';


const Calendar = ({ calendarEvents}) => {




    return (
        <Link to="/Calendar" className="calendar-link-container">
            <div className="calendar-container calendarIcon" title="Calendario" alt="Calendario">

            
            </div>
        </Link>   
    );
}


export default Calendar;