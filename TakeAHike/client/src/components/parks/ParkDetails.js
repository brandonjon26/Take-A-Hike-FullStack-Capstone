import React, { useState, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory, useParams } from "react-router";
import { getParkById } from "../../modules/parkManager";
import { Link } from "react-router-dom";
import "./ParkDetail.css"

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
                <div className="imageSize">
                    <p>
                        <img src={park.imageUrl} alt="display image" />
                    </p>
                </div>
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
                    Address: {park.address}
                </p>
                <p>
                    <a href={park.websiteLink}>
                        Website: {park.websiteLink}
                    </a>
                </p>
                <Link to="/Park">
                    <button className="btn btn-primary">
                        Back
                    </button>
                </Link>
            </CardBody>
        </Card>
    )
}