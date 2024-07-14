import axios from "axios";

const API_URL = 'http://localhost/api/v1/Identity/';

export const login = () => axios.post(`${API_URL}/login`);