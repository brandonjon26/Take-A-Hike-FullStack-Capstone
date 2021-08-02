import { getToken } from "./authManager";
import "firebase/auth";

const baseUrl = '/api/Hike';

export const getAllHikes = () => {
    return fetch(baseUrl)
        .then((res) => res.json())
};