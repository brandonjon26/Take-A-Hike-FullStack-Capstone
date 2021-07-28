import React from "react";
import { Switch, Route } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import { ParkList } from "./parks/ParkList";
// import QuoteList from "./QuoteList";
// import QuoteAddForm from "./QuoteAddForm";

export default function ApplicationViews({ isLoggedIn }) {
    return (
        <main>
            <Switch>
                <Route path="/login">
                    <Login />
                </Route>

                <Route path="/register">
                    <Register />
                </Route>

                <Route path="/Park">
                    <ParkList />
                </Route>
            </Switch>
        </main>
    );
};