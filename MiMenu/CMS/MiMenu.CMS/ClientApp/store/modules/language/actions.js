import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getLanguages = ({ commit }, data) => {
    return HttpService.get(`/api/Language/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {
    }).then(response => {
        commit("GET_LANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getAllLanguages = ({ commit }) => {
    return HttpService.get(`/api/Language/GetAll`).then(response => {
        //console.log(response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/Language/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateLanguage = ({ commit }, data) => {
    return HttpService.put('/api/Language/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addLanguage = ({ commit }, params) => {
    return HttpService.post('/api/Language/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteLanguage = ({ commit }, data) => {
    return HttpService.delete(`/api/Language/Delete?id=${data.Id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getLanguages,
    getLanguage,
    updateLanguage,
    deleteLanguage,
    addLanguage,
    getAllLanguages
}
