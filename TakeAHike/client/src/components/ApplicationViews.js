import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import { ParkList } from "./parks/ParkList";
import { AddParkForm } from "./parks/AddParkForm";
import { EditPark } from "./parks/EditParkForm";
import { ParkDetail } from "./parks/ParkDetails";
import { HikeList } from "./hikes/HikeList";
import { AddHikeForm } from "./hikes/AddHikeForm";
import { EditHike } from "./hikes/EditHikeForm";
import { HikeDetail } from "./hikes/HikeDetails";


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

                <Route exact path="/Park/edit/:id">
                    <EditPark />
                </Route>

                <Route exact path="/Park/details/:id">
                    <ParkDetail />
                </Route>

                <Route exact path="/Hike">
                    <HikeList />
                </Route>

                <Route exact path="/Hike/add">
                    <AddHikeForm />
                </Route>

                <Route exact path="/Hike/edit/:id">
                    <EditHike />
                </Route>
                <Route exact path="/Hike/details/:id">
                    <HikeDetail />
                </Route>
            </Switch>
        </main>
    );
};