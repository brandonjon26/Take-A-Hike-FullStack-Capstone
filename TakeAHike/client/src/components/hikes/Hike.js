import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { deleteHike } from "../../modules/hikeManager";

export const Hikes = ({ hike, getHikes }) => {

    const createTime1 = (hike.dateOfHike.split("T"))
    const time1 = (createTime1.pop())
    const date1 = (createTime1.shift())
    const timeSplit1 = ((time1).split(":"))
    const dateSplit1 = ((date1).split("-"))
    const year1 = (dateSplit1[0])
    const day1 = (dateSplit1[2])
    const month1 = (dateSplit1[1])
    const hour1 = (timeSplit1[0])
    const minute1 = (timeSplit1[1])
    const midnightCheck1 = (hour1 === "00" ? "12" : hour1)
    const nightNoon1 = (hour1 > 12 ? " PM" : " AM")
    const TimeStamp1 = day1 + "/" + month1 + "/" + year1 + " @ " + midnightCheck1 + ":" + minute1 + "" + nightNoon1;

    const handleDeleteHike = (event) => {
        window.confirm(`Are you sure you want to delete this park?`)
        event.preventDefault()
        deleteHike(hike.id)
            .then(() => getHikes())
    }

    return (
        <Card>
            <CardBody>
                <div>
                    <p>
                        <img src={hike.park.imageURL} alt="display image" />
                    </p>
                    <p>
                        <strong>{hike.park.parkName}</strong>
                    </p>
                    <p>
                        {TimeStamp1}
                    </p>
                    <Link to={`/Hike/edit/${hike.id}`}>
                        <button className="btn btn-primary">
                            Edit
                        </button>
                    </Link>
                    <button className="buttonRemoveHike" type="button" onClick={handleDeleteHike}>
                        Delete Park
                    </button>
                </div>
            </CardBody>
        </Card>
    )
}