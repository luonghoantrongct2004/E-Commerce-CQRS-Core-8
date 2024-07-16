import axios from "axios";

export const brandBase = axios.create({
    baseURL: 'http://localhost/api/v1/Brand/',
    headers: {
        'Content-Type': 'application/json'
    }
})

brandBase.interceptors.request.use(
    config => {
        const accessToken = localStorage.getItem('accessToken');
        if (accessToken) {
            config.headers.Authorization = `Bearer ${accessToken}`;
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

export const getBrands = async () => {
    try{
        const response = await brandBase.get();
        return response.data;
    }catch(error) {
        console.error('Error fetching brands:', error);
        throw error;
    }
};

export const createBrand = async (brand) => {
    try {
        const response = await brandBase.post('',brand);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            if (error.response && error.response.data && error.response.data.errors) {
                const errorMessages = error.response.data.errors;
                errorMessages.forEach(errorMessage => {
                    alert(errorMessage);
                });
            } else {
                console.error('Error:', error.message);
                alert('Error: Network error');
            }
        } else {
            console.error('Error:', error.message);
            alert('Error: An unexpected error occurred');
        }
        throw error;
    }
};

export const updateBrand = async (id, brand) => {
    try {
        const response = await brandBase.put(id, brand);
        return response.data;
    } catch (error) {
        console.error('Error updating brand:', error.response.data.message);
        throw error;
    }
};

export const deleteBrand = async (id) => {
    try {
        const response = await brandBase.delete(id);
        return response.data;
    } catch (error) {
        console.error('Error deleting brand:', error.response.data.message);
        throw error;
    }
};