import React, { useState, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory, useParams } from "react-router";
import { getParkById } from "../../modules/parkManager";
import { Link } from "react-router-dom";

export const ParkDetail = () => {
    const { id } = useParams();
    const [park, setPark] = useState({});
    const history = useHistory();

    const getParkDetails = () => {
        getParkById(id)
            .then(setPark)
    }

    useEffect(() => {
        getParkDetails();
    }, []);

    return (
        <Card>
            <CardBody>
                <h3>
                    <strong>Park Name: {park.parkName}</strong>
                </h3>
                <p>
                    Description: {park.description}
                </p>
                <p>
                    Contact Info: {park.contactInfo}
                </p>
                <p>
                    <img src={park.imageURL} alt="display image" />
                </p>
                <p>
                    Address: {park.address}
                </p>
                <p>
                    Website: {park.websiteLink}
                </p>
            </CardBody>
        </Card>
    )
}