import React, { useState, useEffect } from "react";
import { useHistory } from "react-router";
import { Button, Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { deletePark, getAllParks } from "../../modules/parkManager";

export const Parks = ({ park, getParks, userType }) => {
    const history = useHistory();
    const [parks, setParks] = useState([]);

    const handleDeletePark = (event) => {
        window.confirm(`Are you sure you want to delete this park?`)
        event.preventDefault()
        deletePark(park.id)
            .then(() => getParks())
    }

    return (
        <Card >
            <CardBody >
                <div>
                    <p>
                        <strong>{park.parkName}</strong>
                    </p>
                    <p>{park.contactInfo}</p>
                </div>
                {userType === 1 ?
                    <Link to={`/Park/edit/${park.id}`}>
                        <button className="btn btn-primary">
                            Edit
                        </button>
                    </Link>
                    : null}
                <Link to={`/Park/details/${park.id}`}>
                    <button className="btn btn-primary">
                        Details
                    </button>
                </Link>
                {userType === 1 ?
                    <button className="buttonRemovePark" type="button" onClick={handleDeletePark}>
                        Delete Park
                    </button>
                    : null}
            </CardBody>
        </Card >
    )
}