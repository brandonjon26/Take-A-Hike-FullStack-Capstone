import React, { useEffect, useState } from "react";
import { Hikes } from "./Hike";
import { getAllHikes } from "../../modules/hikeManager";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";


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
                <div className="list">
                    <h3>My Hikes!</h3>
                    <Link to="/Hike/add">
                        <button className="btn btn-primary">
                            Take A Hike!
                        </button>
                    </Link>
                    <div className="listComponent">
                        {hikes.map((h) => {
                            return <Hikes hike={h} key={h.id} getHikes={getHikes} />
                        })}
                    </div>
                </div>
            </div>
        </>
    )
}