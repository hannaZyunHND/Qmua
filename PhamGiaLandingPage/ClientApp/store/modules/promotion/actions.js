import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getPromotions = ({ commit }, data) => {
    return HttpService.post(`/api/Promotion/Get`, data).then(response => {
        commit("GET_PROMOTIONS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getPromotion = ({ commit }, id) => {
    return HttpService.get(`/api/Promotion/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getNamePromotion = ({ commit }) => {
    return HttpService.get(`/api/Promotion/GetAllNamePromotion`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updatePromotion = ({ commit }, data) => {
    return HttpService.put('/api/Promotion/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addPromotion = ({ commit }, params) => {
    return HttpService.post('/api/Promotion/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deletePromotion = ({ commit }, data) => {
    return HttpService.put(`/api/Promotion/UpdateStatus?id=${data.id}&status=${data.status}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const promotiontTypeGet = ({ commit }, data) => {
    return HttpService.get('/api/Common/PromotionTypeGet')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getPromotions,
    getPromotion,
    updatePromotion,
    deletePromotion,
    addPromotion,
    promotiontTypeGet,
    getNamePromotion
}
