import React from "react";
import { useHistory } from "react-router";
import { Button, Card, CardBody } from "reactstrap";
import { getAllTags } from "../../modules/parkManager";

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
            </CardBody>
        </Card>
    )
}