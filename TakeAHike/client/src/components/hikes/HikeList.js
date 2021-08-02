import React, { useEffect, useState } from "react";
import { Hikes } from "./Hike";
import { getAllHikes } from "../../modules/hikeManager";
import { useHistory } from "react-router";


export const HikeList = () => {
    const history = useHistory();
    const [hikes, setHikes] = useState([]);

    const getHikes = () => {
        getAllHikes().then(hikes => setHikes(hikes))
    }

    useEffect(() => {
        getHikes();
    }, []);

    return (
        <>
            <div className="container">
                <div className="row justify-content-center">
                    <h3>My Hikes!</h3>
                    {hikes.map((h) => {
                        return <Hikes hike={h} key={h.id} />
                    })}
                </div>
            </div>
        </>
    )
}