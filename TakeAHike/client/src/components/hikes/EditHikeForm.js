import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { editHike, getHikeById } from "../../modules/hikeManager";
import { Form, FormGroup, Button, Container } from "reactstrap";
import { getAllParks } from "../../modules/parkManager";

export const EditHike = () => {
    const [parks, setParks] = useState([])
    const [hike, setHike] = useState({})
    const { id } = useParams();
    const history = useHistory();

    const handleFieldChange = (event) => {
        const newHike = { ...hike }
        let selectedVal = event.target.value
        newHike[event.target.id] = selectedVal
        setHike(newHike)
    }

    const handleSaveEvent = (event) => {
        event.preventDefault()
        if (hike.parkId.parkName === "") {
            window.alert("Please fill in all fields")
        } else {
            editHike(hike)
                .then(() => history.push('/Hike'))
        }
    }

    const handleCancelSave = (event) => {
        event.preventDefault()
        history.push("/")
    }

    useEffect(() => {
        getAllParks()
            .then(res => {
                setParks(res)
            })
    }, [])

    useEffect(() => {
        getHikeById(id).then(setHike)
    }, [id])

    return (
        <form className="addHikeForm">
            <h2 className="addHikeForm_title">Edit This Hike</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="park">New Park: </label>
                    <select value={hike.parkId} name="parkId" id="parkId" onChange={handleFieldChange} className="form-control">
                        <option value="0">Select A Park</option>
                        {parks.map(p => (
                            <option key={p.id} value={p.id}>
                                {p.parkName}
                            </option>
                        ))}
                    </select>
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="dateOfHike">New Date: </label>
                    <input
                        type="date"
                        id="dateOfHike"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Date of your Hike!"
                        value={hike.dateOfHike}
                    />
                </div>
            </fieldset>
            <Button className="article-btn"
                onClick={handleSaveEvent}>
                Save Hike
            </Button>
            <Button className="article-btn"
                onClick={handleCancelSave}>
                Cancel
            </Button>
        </form>
    )
}