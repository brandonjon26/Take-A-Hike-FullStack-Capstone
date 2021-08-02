import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

export const Hikes = ({ hike }) => {

    return (
        <Card>
            <CardBody>
                <div>
                    <p>
                        <img src={myHikes.park.imageURL} alt="display image" />
                    </p>
                    <p>
                        <strong>{myHikes.park.parkName}</strong>
                    </p>
                    <p>
                        {myHikes.dateOfHike}
                    </p>
                </div>
            </CardBody>
        </Card>
    )
}