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

export const getHikeById = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            }
        }).then(resp => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error("An unknown error occured while getting this hike.")
            }
        });
    });
}

export const editPark = (hike) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${hike.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "Application/json"
            },
            body: JSON.stringify(hike)
        })
    });
}

