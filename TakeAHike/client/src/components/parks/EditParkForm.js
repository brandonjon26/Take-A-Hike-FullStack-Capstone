import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { editPark, getParkById } from "../../modules/parkManager";
import { Form, FormGroup, Button, Container } from "reactstrap";

export const EditPark = () => {
    const [park, setPark] = useState({})
    const { id } = useParams();
    const history = useHistory();

    const handleFieldChange = (event) => {
        const newPark = { ...park }
        let selectedVal = event.target.value
        newPark[event.target.id] = selectedVal
        setPark(newPark)
    }

    const handleSaveEvent = (event) => {
        event.preventDefault()
        if (park.parkName === "") {
            window.alert("Please fill in all fields")
        } else {
            editPark(park)
                .then(() => history.push('/Park'))
        }
    }

    const handleCancelSave = (event) => {
        event.preventDefault()
        history.push("/Park")
    }

    useEffect(() => {
        getParkById(id).then(setPark)
    }, [id])

    return (
        <form className="addParkForm">
            <h2 className="addParkForm_title">Edit This Park</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="parkName">Park: </label>
                    <input
                        type="text"
                        id="parkName"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Park Name"
                        value={park.parkName}
                    />
                </div>
                <div>
                    <label htmlFor="description">Description: </label>
                    <input
                        type="text"
                        id="description"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Park Description"
                        value={park.description}
                    />
                </div>
                <div>
                    <label htmlFor="contactInfo">Contact Info: </label>
                    <input
                        type="text"
                        id="contactInfo"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Contact Info"
                        value={park.contactInfo}
                    />
                </div>
                <div>
                    <label htmlFor="imageURL">Image: </label>
                    <input
                        type="text"
                        id="imageURL"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Image Of Park"
                        value={park.imageURL}
                    />
                </div>
                <div>
                    <label htmlFor="address">Address: </label>
                    <input
                        type="text"
                        id="address"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Park Address"
                        value={park.address}
                    />
                </div>
                <div>
                    <label htmlFor="websiteLink">Website: </label>
                    <input
                        type="text"
                        id="websiteLink"
                        onChange={handleFieldChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Park Website"
                        value={park.websiteLink}
                    />
                </div>
            </fieldset>
            <Button className="article-btn"
                onClick={handleSaveEvent}>
                Save Park
            </Button>
            <Button className="article-btn"
                onClick={handleCancelSave}>
                Cancel
            </Button>
        </form>
    )
}