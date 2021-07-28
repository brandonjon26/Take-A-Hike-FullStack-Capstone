import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { addPark } from "../../modules/parkManager";

export const AddParkForm = () => {
    const [par, setPark] = useState({
        parkName: "",
        description: "",
        contactInfo: "",
        imageURL: "",
        address: "",
        websiteLink: ""
    });
    consy[isLoading, setIsLoading] = useState(false);
    const history = useHistory();

    const handleFieldChange = (event) => {
        const newPark = { ...park }
        let selectedVal = event.target.value
        if (event.target.id.includes("Id")) {
            selectedVal = parseInt(selectedVal)
        }
        newPark[event.target.id] = selectedVal
        setPark(newPark)
    }

    const handleClickSavePark = (event) => {
        event.preventDefault()

        addPark(park)
            .then(() => setPark({
                parkName: "",
                description: "",
                contactInfo: "",
                imageURL: "",
                address: "",
                websiteLink: ""
            })).then((p) => {
                history.push("/Park");
            })
    }

    return (
        <form className="addParkForm">
            <h2 className="addParkForm_title">Add A Park!</h2>
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
            <button className="btn btn-primary"
                onClick={handleClickSavePark}>
                Save Park
            </button>
        </form>
    )
}