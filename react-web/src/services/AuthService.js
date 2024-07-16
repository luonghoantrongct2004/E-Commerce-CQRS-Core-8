import axios from "axios";

export const authBase = axios.create({
    baseURL: 'http://localhost/api/v1/Identity/',
    headers: {
        'Content-Type': 'application/json'
    }
})

authBase.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;
        if(error.response.status == 401 && !originalRequest._retry){
            originalRequest._retry = true;
            try {
                const refreshToken = localStorage.getItem('refreshToken');
                const response = await authBase.post('/RefreshToken', {refreshToken});
                const newAccessToken = response.data.token;
                localStorage.setItem('accessToken', newAccessToken);
                authBase.defaults.headers['Authorization'] = 'Bearer' + newAccessToken;
                originalRequest.headers['Authorization'] = 'Bearer' + newAccessToken;
                return authBase(originalRequest);
            } catch (error) {
                console.error('Refresh token is invalid', error);
                window.location.href = '/login';
            }
        }
        return Promise.reject(error);
    }
)

export const register = async (userData) => {
    try {
        const response = await authBase.post('/register', userData);
        return response.data;
    } catch (error) {
        throw error;
    }
}

export const login = async (credentials) => {
    try {
        const response = await authBase.post('/login', credentials);
        const {token, refreshToken} = response.data;
        localStorage.setItem('accessToken', token);
        localStorage.setItem('refreshToken', refreshToken);
        authBase.defaults.headers['Authorization'] = 'Bearer ' + token;
        return response.data;
    } catch (error) {
        throw error;
    }
}

export const refreshAccessToken = async () => {
    try {
        const refreshToken = localStorage.getItem('refreshToken');
        const response = await authBase.post('/refreshToken', {refreshToken});
        const {token, refreshToken : newRefreshToken} = response.data;
        localStorage.setItem('accessToken', token);
        localStorage.setItem('refreshToken', newRefreshToken);
        authBase.defaults.headers['Authorization'] = 'Bearer ' + token;
        return response.data;
    } catch (error) {
        throw error;
    }
}