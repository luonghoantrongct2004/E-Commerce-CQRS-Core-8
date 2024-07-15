import axios from "axios";

const API = 'http://localhost/api/v1/Brand/';

export const getBrands = async () => {
    try{
        const response = await axios.get(`${API}`);
        return response.data;
    }catch(error) {
        console.error('Error fetching brands:', error);
        throw error;
    }
};

export const createBrand = async (brand) => {
    try {
        const response = await axios.post(`${API}`, brand);
        return response.data;
    } catch (error) {
        console.error('Error creating brand:', error);
        throw error;
    }
};

export const updateBrand = async (id, brand) => {
    try {
        const response = await axios.put(`${API}${id}`, brand);
        return response.data;
    } catch (error) {
        console.error('Error updating brand:', error);
        throw error;
    }
};

export const deleteBrand = async (id) => {
    try {
        const response = await axios.delete(`${API}${id}`);
        return response.data;
    } catch (error) {
        console.error('Error deleting brand:', error);
        throw error;
    }
};