import React from "react";
import { useHistory } from "react-router";
import { Button, Card, CardBody } from "reactstrap";
import { getAllTags } from "../../modules/parkManager";
import { Link } from "react-router-dom";

export const Parks = ({ park, handleDeletePark }) => {
    const history = useHistory();

    if (park.isDeleted === false) {
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
                        <button className="buttonRemovePark" type="button" onClick={() => handleDeletePark(park.id)}>
                            Delete Park
                        </button>
                    </Link>
                </CardBody>
            </Card >
        );
    } else {
        return <> </>
    }
}