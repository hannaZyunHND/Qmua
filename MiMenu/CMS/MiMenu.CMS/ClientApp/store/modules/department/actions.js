import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getDepartments = ({ commit }, data) => {
    return HttpService.post(`/api/Department/Get`, data).then(response => {
        commit("GET_DEPARTMENTS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getDepartment = ({ commit }, id) => {
    return HttpService.get(`/api/Department/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateDepartment = ({ commit }, data) => {
    return HttpService.put('/api/Department/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addDepartment = ({ commit }, params) => {
    return HttpService.post('/api/Department/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteDepartment = ({ commit }, data) => {
    return HttpService.delete(`/api/Department/Delete?id=${data.id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getDepartments,
    getDepartment,
    updateDepartment,
    deleteDepartment,
    addDepartment
}
