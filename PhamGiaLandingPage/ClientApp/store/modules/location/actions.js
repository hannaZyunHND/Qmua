import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getLocations = ({ commit }, data) => {
    return HttpService.post(`/api/Location/Get`, data).then(response => {
        commit("GET_LOCATIONS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getLocationAll = ({ commit }) => {
    return HttpService.get(`/api/Location/GetAll`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getLocation = ({ commit }, id) => {
    return HttpService.get(`/api/Location/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateLocation = ({ commit }, data) => {
    return HttpService.put('/api/Location/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addLocation = ({ commit }, params) => {
    return HttpService.post('/api/Location/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteLocation = ({ commit }, data) => {
    return HttpService.delete(`/api/Location/Delete?id=${data.id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const LocationGetGroupId = ({ commit }, data) => {
    return HttpService.get('/api/Common/LocationGetGroupId')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getLocations,
    getLocation,
    updateLocation,
    deleteLocation,
    addLocation,
    LocationGetGroupId,
    getLocationAll
}
