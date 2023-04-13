import React from "react";
import { BrowserRouter, Route } from "react-router-dom";
import Home from "./pages/Home/Home"


const App = () => {

    return (
        <BrowserRouter>
            <Route path="/">
                <Home />
            </Route>
        </BrowserRouter>
    )

}


export default App;