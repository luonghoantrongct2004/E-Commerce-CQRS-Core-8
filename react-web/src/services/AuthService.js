import axios from "axios";
import { login } from "../api/AuthApi";

const login = (username, password) => {
    return axios.post(login, {username, password})
        .then(response => {
            if(response.data.token){
                localStorage.setItem('user', JSON.stringify(response.data))
            }
            return response.data;
        });
};

export default {login};
