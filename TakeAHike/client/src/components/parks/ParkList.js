import React, { useEffect, useState } from "react";
import { Parks } from "./Park";
import { getAllParks, deletePark } from "../../modules/parkManager";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";
import { getUserType } from "../../modules/authManager";

export const ParkList = () => {
    const history = useHistory();
    const [parks, setParks] = useState([]);
    const [userType, setUserType] = useState();

    const getParks = () => {
        getAllParks().then(parks => setParks(parks))
    }

    useEffect(() => {
        getParks();
    }, []);

    useEffect(() => {
        getUserType()
            .then((resp) => {
                setUserType(resp.userTypeId)
                console.log(resp)
            })
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
                        return <Parks park={p} key={p.id} getParks={getParks} userType={userType} />
                    })}
                </div>
            </div>
        </>
    )
}