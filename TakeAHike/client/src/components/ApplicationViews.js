import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import { ParkList } from "./parks/ParkList";
import { AddParkForm } from "./parks/AddParkForm";


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

                <Route path="/" exact>
                    {isLoggedIn ? <Home /> : <Redirect to="/login" />}
                </Route>

                <Route path="/Park/add" exact>
                    <AddParkForm />
                </Route>

                <Route path="/Park" exact>
                    <ParkList />
                </Route>
            </Switch>
        </main>
    );
};