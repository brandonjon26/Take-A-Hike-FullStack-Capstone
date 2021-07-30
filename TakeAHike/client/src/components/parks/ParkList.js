import React, { useEffect, useState } from "react";
import { Parks } from "./Park";
import { getAllParks, deletePark } from "../../modules/parkManager";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";

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
                    <Link to="/Park/add">
                        <button className="btn btn-primary">
                            Add A Park
                        </button>
                    </Link>
                    {parks.map((p) => {
                        return <Parks park={p} key={p.id} />
                    })}
                </div>
            </div>
        </>
    )
}