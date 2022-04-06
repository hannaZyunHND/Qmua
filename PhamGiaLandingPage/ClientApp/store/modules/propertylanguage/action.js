import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getPropertyLanguages = ({ commit }, data) => {
    HttpService.get(`/api/PropertyLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_PROPERTYLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getPropertyLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/PropertyLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addPropertyLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/PropertyLanguage/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getPropertyLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/PropertyLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editPropertyLanguage = ({ commit }, params) => {
    return HttpService.put('/api/PropertyLanguage/Update', params
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
    getPropertyLanguages,
    addPropertyLanguage,
    getPropertyLanguage,
    editPropertyLanguage,
    getPropertyLanguageAll
}
