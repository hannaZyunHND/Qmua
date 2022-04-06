import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getPromotionInLanguages = ({ commit }, data) => {
    HttpService.get(`/api/PromotionInLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_PROMOTIONINLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getPromotionInLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/PromotionInLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addPromotionInLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/PromotionInLanguage/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getPromotionInLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/PromotionInLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editPromotionInLanguage = ({ commit }, params) => {
    return HttpService.put('/api/PromotionInLanguage/Update', params
    ).then(response => {
        return response.data;
    })
        .catch(e => {
            alert('ex found:' + e)
        })
}
//const removeAds = ({ commit }, data) => {
//    return HttpService.delete(`/api/Ads/Delete?id=${data.Id}`)
//        .then(response => {
//            return response.data;
//        })
//        .catch(e => {
//            alert('ex found:' + e)
//        })
//}

export default {
    getPromotionInLanguages,
    addPromotionInLanguage,
    getPromotionInLanguage,
    editPromotionInLanguage,
    getPromotionInLanguageAll
}
