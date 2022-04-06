import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getManufacturers = ({ commit }, data) => {
    return HttpService.get(`/api/Manufacturer/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {
    }).then(response => {
        commit("GET_MANUFACTURERSS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getAllManufacturers = ({ commit }) => {
    return HttpService.get(`/api/Manufacturer/GetAll`).then(response => {
        //console.log(response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getManufacturer = ({ commit }, id) => {
    return HttpService.get(`/api/Manufacturer/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateManufacturer = ({ commit }, data) => {
    return HttpService.put('/api/Manufacturer/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addManufacturer = ({ commit }, params) => {
    return HttpService.post('/api/Manufacturer/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteManufacturer = ({ commit }, data) => {
    return HttpService.delete(`/api/Manufacturer/Delete?id=${data.Id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getManufacturers,
    getManufacturer,
    updateManufacturer,
    deleteManufacturer,
    addManufacturer,
    getAllManufacturers
}
