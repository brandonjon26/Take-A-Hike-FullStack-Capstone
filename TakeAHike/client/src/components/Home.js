import React from "react";
import { HikeList } from "./hikes/HikeList";
import { getToken } from "../modules/authManager";
import { useEffect, useState } from "react";
import { useParams } from "react-router";

export default function Hello() {
    return (
        <div className="header">
            <h1>Take A Hike!</h1>

            <p>
                At "Take A Hike!", we aim to give users a great experience and easy way to view parks/trails in their
                area, while also allowing them to plan their next hiking trip. Our mission is simple, we want to give
                people with the same love of hiking an easy way to share their passion. What if you know or have a
                park that is not on the website? Simple, just go add it via our "Our Parks" tab! We hope that you
                find our website simple and straight to the point. Now, quit reading and Take A Hike!
            </p>
            <HikeList />
        </div>
    );
}