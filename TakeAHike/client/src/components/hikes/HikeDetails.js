import React, { useState, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory, useParams } from "react-router";
import { getHikeById } from "../../modules/hikeManager";
import { Link } from "react-router-dom";
import "./HikeDetail.css"

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
                <div>
                    <p>
                        <img src={hike.park?.imageUrl} alt="display image" className="imageDetail" />
                    </p>
                </div>
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
                    Address: {hike.park?.address}
                </p>
                <p>
                    <a href={hike.park?.websiteLink}>
                        Website: {hike.park?.websiteLink}
                    </a>
                </p>
                <p>
                    Date: {hike.dateOfHike}
                </p>
                <Link to="/">
                    <button className="btn btn-primary">
                        Back
                    </button>
                </Link>
            </CardBody>
        </Card>
    )
}