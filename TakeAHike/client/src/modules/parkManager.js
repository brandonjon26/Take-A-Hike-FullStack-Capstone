import { getToken } from "./authManager";

const baseUrl = '/api/Park';

export const getAllParks = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(resp => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error("An unknown error occured while trying to get parks")
            }
        });
    });
};