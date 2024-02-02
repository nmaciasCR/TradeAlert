import React from "react";
import Styles from "./NotificationIcon.css";
import { Link } from 'react-router-dom';


const NotificationIcon = ({ notifications }) => {
    const notificationCount = (notifications ?? []).length;
    const bellIconClass = notificationCount == 0 ? "NotificationIcon-bell" : "NotificationIcon-bell-active";

    return (
        <Link to="/Notifications" className="Link-container">
            <div className={`NotificationIcon-container ${bellIconClass}`} title="Notificaciones" alt="Notificaciones">
                {
                    notificationCount != 0 ? (
                        <div className="count">
                            {notificationCount}
                        </div>
                    ) : ""
                }
            </div>
        </Link>
    )
}

export default NotificationIcon;
