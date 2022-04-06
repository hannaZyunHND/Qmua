import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getProductInLanguages = ({ commit }, data) => {
    HttpService.get(`/api/ProductInLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_PRODUCTINLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getProductInLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/ProductInLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addProductInLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/ProductInLanguage/Add`, params)
        .then(response => {
            console.log(response);
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getProductInLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/ProductInLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editProductInLanguage = ({ commit }, params) => {
    return HttpService.put('/api/ProductInLanguage/Update', params
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
    getProductInLanguages,
    addProductInLanguage,
    getProductInLanguage,
    editProductInLanguage,
    getProductInLanguageAll

}
