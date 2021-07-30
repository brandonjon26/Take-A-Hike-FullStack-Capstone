import React from "react";
import { useHistory } from "react-router";
import { Button, Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { deletePark, getAllParks } from "../../modules/parkManager";

export const Parks = ({ park }) => {
    const history = useHistory();

    const handleDeletePark = (event) => {
        window.confirm(`Are you sure you want to delete this park?`)
        event.preventDefault()
        deletePark(park.id)
            .then(() => history.push("/Park/"))
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
                <Link to={`/Park/edit/${park.id}`}>
                    <button className="btn btn-primary">
                        Edit
                    </button>
                </Link>
                <button className="buttonRemovePark" type="button" onClick={handleDeletePark}>
                    Delete Park
                </button>
            </CardBody>
        </Card >
    )
}