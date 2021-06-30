import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:5001/'
})

instance.interceptors.response.use(response => response, error => {
    alert("An error occurred: " + error);
})

export default instance;