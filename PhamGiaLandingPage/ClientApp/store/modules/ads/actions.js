import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getAdss = ({ commit }, data) => {
    HttpService.get(`/api/Ads/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {
        console.log(response);
        commit("GET_ADSS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAds = ({ commit }, id) => {
    return HttpService.get(`/api/Ads/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const addAds = ({ commit }, params) => {
    return HttpService.post('/api/Ads/add', params, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const editAds = ({ commit }, params) => {
    let config = {

    };
    return HttpService.put('/api/Ads/Update', params, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const removeAds = ({ commit }, data) => {
    return HttpService.delete(`/api/Ads/Delete?id=${data.Id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getAds,
    addAds,
    editAds,
    getAdss,
    removeAds

}
