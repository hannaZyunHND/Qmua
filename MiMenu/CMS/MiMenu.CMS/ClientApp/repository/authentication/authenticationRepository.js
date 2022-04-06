import {handleResponse} from "../../helpers/handle-response";
import HttpService from "../../plugins/http";
import {router} from "../../router";

let currentUserSubject = JSON.parse(localStorage.getItem('currentUser'));

export const authenticationRepository = {
    login,
    logout,
    getCurrentUser,
    headers,
    get currentUserValue() {
        return currentUserSubject
    },

};

function headers() {
    const currentUser = currentUserSubject || {};
    return currentUser.token ? {'Authorization': 'Bearer ' + currentUser.token} : {};
}

function login(username, password) {
    return HttpService.post(`/api/account/login` ,{username, password})
        .then(handleResponse)
        .then(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject=JSON.parse(localStorage.getItem('currentUser'));
            return user;
        });
}

async function getCurrentUser() {
    let response=await HttpService.get(`/api/user/profile`).catch(e => {
        localStorage.removeItem('currentUser');
        router.push('/admin/login');
        });
    return response.data;
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject= JSON.parse(localStorage.getItem('currentUser'));
}
