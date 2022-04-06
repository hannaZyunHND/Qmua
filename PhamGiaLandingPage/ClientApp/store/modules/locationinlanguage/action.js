import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getLocationInLanguages = ({ commit }, data) => {
    HttpService.get(`/api/LocationInLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_LOCATIONSLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getLocationInLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/LocationInLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addLocationInLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/LocationInLanguage/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getLocationInLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/LocationInLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editLocationInLanguage = ({ commit }, params) => {
    return HttpService.put('/api/LocationInLanguage/Update', params
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
    getLocationInLanguages,
    addLocationInLanguage,
    getLocationInLanguage,
    editLocationInLanguage,
    getLocationInLanguageAll
}
