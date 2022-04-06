import axios from 'axios';
import {authenticationRepository} from "../repository/authentication/authenticationRepository";

//import  AppSettings  from './../../appsettings.json';
//const Redis = require('./../../appsettings.json');

let BASE_URL = '';
if (process.env.BASE_URL) {
    BASE_URL = process.env.BASE_URL;
} else {
    //BASE_URL = window.AppSettings.Domain;
}
const HttpService = axios.create({
    baseURL: BASE_URL,
    headers: {'Content-Type': 'application/json'},
});
HttpService.interceptors.request.use(
    function (config) {
        const token = headers();
        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

function headers() {
    const currentUser = authenticationRepository.currentUserValue || {};
    return currentUser.token ? currentUser.token : '';
}

export default HttpService;


