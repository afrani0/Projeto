import axios from 'axios';

const api = axios.create({
    baseURL : '[SUA_URL]',
})

export default api;