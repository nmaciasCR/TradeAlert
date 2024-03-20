import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Home from "./pages/Home/Home";
import Stocks from "./pages/Stocks/Stocks";
import Portfolio from "./pages/Portfolio/Portfolio";
import Notifications from "./pages/Notifications/Notifications";
import Calendar from "./pages/Calendar/Calendar";

const App = () => {

    return (
        <BrowserRouter>
            <Switch>
                <Route path="/Stocks">
                    <Stocks />
                </Route>
                <Route path="/Home">
                    <Home />
                </Route>
                <Route path="/Portfolio">
                    <Portfolio />
                </Route>
                <Route path="/Notifications">
                    <Notifications />
                </Route>
                <Route path="/Calendar">
                    <Calendar />
                </Route>
                <Route path="/">
                    <Home />
                </Route>
            </Switch>
        </BrowserRouter>
    )

}


export default App;