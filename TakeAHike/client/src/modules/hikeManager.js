import { getToken } from "./authManager";
import "firebase/auth";

const baseUrl = '/api/Hike';

export const getAllHikes = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => res.json())
    });
};

export const addHike = (hike) => {
    return getToken().then((token) => {
        fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(hike),
        })
    });
};

