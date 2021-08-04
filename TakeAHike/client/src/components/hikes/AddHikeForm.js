import React, { useState, useEffect } from "react";
import { useHistory, Link } from "react-router-dom";
import { addHike } from "../../modules/hikeManager";
import { getAllParks } from "../../modules/parkManager";

export const AddHikeForm = () => {
    const [parks, setParks] = useState([])
    const [hike, setHike] = useState({
        parkId: 0,
        dateOfHike: ""
    });
    const [isLoading, setIsLoading] = useState(false);
    const history = useHistory();

    const handleFieldInputChange = (event) => {
        const newHike = { ...hike }
        let selectedVal = event.target.value
        if (event.target.id.includes("Id")) {
            selectedVal = parseInt(selectedVal)
        }
        newHike[event.target.id] = selectedVal
        setHike(newHike)
    }

    useEffect(() => {
        getAllParks()
            .then(res => {
                setParks(res)
            })
    }, [])

    const handleClickSaveHike = (event) => {
        event.preventDefault()

        addHike(hike)
            .then((h) => {
                history.push("/Hike");
            })
    }

    return (
        <form className="addHikeForm">
            <h2 className="addHikeForm_title">Take A Hike!</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="park">Park: </label>
                    <select value={hike.parkId} name="parkId" id="parkId" onChange={handleFieldInputChange} className="form-control">
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
                    <label htmlFor="dateOfHike">Select A Date: </label>
                    <input
                        type="date"
                        id="dateOfHike"
                        onChange={handleFieldInputChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Date of your Hike!"
                        value={hike.dateOfHike}
                    />
                </div>
            </fieldset>
            <button className="btn btn-primary"
                onClick={handleClickSaveHike}>
                Save Hike
            </button>
            <Link to="/Hike">
                <button className="btn btn-primary">
                    Back
                </button>
            </Link>
        </form>
    )
}