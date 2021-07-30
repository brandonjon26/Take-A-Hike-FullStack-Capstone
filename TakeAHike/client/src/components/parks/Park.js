import React from "react";
import { useHistory } from "react-router";
import { Button, Card, CardBody } from "reactstrap";
import { getAllTags } from "../../modules/parkManager";
import { Link } from "react-router-dom";

export const Parks = ({ park }) => {
    const history = useHistory();

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
            </CardBody>
        </Card >
    )
}