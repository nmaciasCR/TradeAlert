import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Calendar.css";
import Header from "../../components/Header/Header";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import PortfolioTable from '../../components/PortfolioTable/PortfolioTable.jsx';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Table from 'react-bootstrap/Table';
import moment from "moment";


var momentLib = require('moment');

//Retorna el dia de los eventos del calendario (Ej: Lunes 18 de Marzo)
const GetEventDayTitle = (date) => {
    return momentLib(date, momentLib.ISO_8601).format("dddd D [de] MMMM");
}

//Formato para mostrar la hora del evento
const GetTimeFormat = (date) => {
    return momentLib(date, momentLib.ISO_8601).format("HH:mm");
}

//Indica si la fecha es hoy
const isToday = (date) => {
    // Obtener la fecha de hoy
    var today = (new Date()).setHours(0, 0, 0, 0);

    // Obtener la fecha sin la parte de la hora para comparar solo el día
    var avaluateDay = (new Date(date)).setHours(0, 0, 0, 0);

    // Comparar si las dos fechas son iguales
    return today === avaluateDay;
}


const Calendar = () => {

    const [calendarList, setCalendarList] = useState([]);

    const loadCalendar = useCallback(() => {
        fetch("api/Calendar/GetCurrentList")
            .then(response => { return response.json() })
            .then(responseJson => {
                setCalendarList(responseJson);
            })
            .catch(error => {
                console.log(error);
            })
    }, []);


    useEffect(() => {
        loadCalendar();
    }, [loadCalendar]);

    //Retorna la clase css para la tabla en caso de que sean los eventos del dia de hoy
    const GetTableStyleToday = (date) => {
        if (isToday(date)) {
            return "event-day-title-today";
        }
        else return "";
    }


    const CreateCalendarCard = (cal) => {


        return (
            Object.entries(cal).map(([key, values]) => (
                <React.Fragment>
                    <Table className={`events-table ${isToday(key) ? 'events-table-today' : ''}`} key={key}>
                        <tbody>
                            <tr>
                                <td colSpan={2} className="event-day-title event-day-title-today">{GetEventDayTitle(key)}</td>
                            </tr>
                            {
                                values.map(item => (
                                    <tr key={item.ID} className="event-item-today">
                                        <td className="event-time-column">{GetTimeFormat(item.scheduleDate)} hs.</td>
                                        <td>{item.description}</td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </Table>
                </React.Fragment>
            ))
        )
    }


    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="Calendar">
                <div className="container-fluid">
                    <Row>
                        <Col sm={2}>
                            <PortfolioTable />
                        </Col>
                        <Col sm={10} className="calendar-body">
                            <div className="Title">Calendario</div>
                            <br />
                            <div className="events-container">
                                {
                                    calendarList ? CreateCalendarCard(calendarList) : (<div>Sin eventos en el calendario</div>)
                                }
                            </div>
                        </Col>
                    </Row>
                </div>
            </div>
        </div>
    )

} //Calendar()

export default Calendar;