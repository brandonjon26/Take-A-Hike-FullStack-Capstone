import React, { useEffect, useState } from "react";
import { Parks } from "./Park";
import { getAllParks } from "../../modules/parkManager";
import { useHistory } from "react-router";

export const ParkList = () => {
    const history = useHistory();
    const [parks, setParks] = useState([]);

    useEffect(() => {
        getAllParks().then(setParks);
    }, []);

    return (
        <>
            <div className="container">
                <div className="row justify-content-center">
                    <h3>Parks</h3>
                    {parks.map((p) => {
                        return <Parks park={p} key={p.id} />
                    })}
                </div>
            </div>
        </>
    )
}