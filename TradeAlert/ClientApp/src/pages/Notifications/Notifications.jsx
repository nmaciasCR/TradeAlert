import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Notifications.css";
import Header from "../../components/Header/Header";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import PortfolioTable from '../../components/PortfolioTable/PortfolioTable.jsx';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Support, Resistor } from "./components/Cards/Cards.jsx";


const SUPPORT_TYPE = 1;
const RESISTOR_TYPE = 2;
const CALENDAR_TYPE = 3;



//Retorna un componente de notificacion segun tipo
const CreateNotificationCard = (noti, refreshNotifications) => {

    switch (noti.notificationTypeId) {
        case SUPPORT_TYPE:
            return (<Support
                id={noti.id}
                entryDate={noti.entryDate}
                typeId={noti.notificationTypeId}
                title={noti.title}
                description={noti.description}
                active={noti.active}
                refresList={refreshNotifications}
            />)
        case RESISTOR_TYPE:
            return (<Resistor
                id={noti.id}
                entryDate={noti.entryDate}
                typeId={noti.notificationTypeId}
                title={noti.title}
                description={noti.description}
                active={noti.active}
                refresList={refreshNotifications}
            />)
        case CALENDAR_TYPE:
            return ("No se puede mostrar la notificacion")
        default:
            return ("No se puede mostrar la notificacion")

    }

}

const Notifications = () => {

    const [notifications, setNotification] = useState([]);

    const loadNotifications = useCallback(() => {
        fetch("api/Notifications/GetEnabledNotifications")
            .then(response => { return response.json() })
            .then(responseJson => {
                setNotification(responseJson);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        loadNotifications();
    }, [loadNotifications]);





    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="Notifications">
                <div className="container-fluid">
                    <Row>
                        <Col sm={2}>
                            <PortfolioTable />
                        </Col>
                        <Col sm={10}>
                            <div className="Title">Notificaciones</div>
                            <br />
                            <div className="Box-Container">
                                {
                                    notifications.map(noti => (CreateNotificationCard(noti, loadNotifications)))
                                }

                            </div>
                        </Col>
                    </Row>
                </div>
            </div>
        </div>)
}



export default Notifications;