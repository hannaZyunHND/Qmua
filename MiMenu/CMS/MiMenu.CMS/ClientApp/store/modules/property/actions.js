import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getPropertys = ({ commit }, data) => {
    return HttpService.post(`/api/Property/Get`, data).then(response => {
        console.log(response.data);
        commit("GET_PROPERTYS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getProperty = ({ commit }, id) => {
    return HttpService.get(`/api/Property/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateProperty = ({ commit }, data) => {
    return HttpService.put('/api/Property/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addProperty = ({ commit }, params) => {
    return HttpService.post('/api/Property/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteProperty = ({ commit }, data) => {
    return HttpService.delete(`/api/Property/Delete?id=${data.id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const propertyGetGroupId = ({ commit }, data) => {
    return HttpService.get('/api/Common/PropertyGetGroupId')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const propertyGetPosition = ({ commit }, data) => {
    return HttpService.get('/api/Common/PropertyGetPosition')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getPropertys,
    getProperty,
    updateProperty,
    deleteProperty,
    addProperty,
    propertyGetGroupId,
    propertyGetPosition
}
