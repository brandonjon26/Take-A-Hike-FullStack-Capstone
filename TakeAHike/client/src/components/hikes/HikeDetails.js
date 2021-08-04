import React, { useState, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory, useParams } from "react-router";
import { getHikeById } from "../../modules/hikeManager";
import { Link } from "react-router-dom";

export const HikeDetail = () => {
    const { id } = useParams();
    const [hike, setHike] = useState({});

    const getHikeDetails = () => {
        getHikeById(id)
            .then(setHike)
    }

    useEffect(() => {
        getHikeDetails();
    }, []);

    return (
        <Card>
            <CardBody>
                <h3>
                    <strong>Park Name: {hike.park?.parkName}</strong>
                </h3>
                <p>
                    Description: {hike.park?.description}
                </p>
                <p>
                    Contact Info: {hike.park?.contactInfo}
                </p>
                <p>
                    <img src={hike.park?.imageURL} alt="display image" />
                </p>
                <p>
                    Address: {hike.park?.address}
                </p>
                <p>
                    Website: {hike.park?.websiteLink}
                </p>
                <p>
                    Date: {hike.dateOfHike}
                </p>
                <Link to="/Hike">
                    <button className="btn btn-primary">
                        Back
                    </button>
                </Link>
            </CardBody>
        </Card>
    )
}