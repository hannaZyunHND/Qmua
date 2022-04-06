import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getDepartmentInLanguages = ({ commit }, data) => {
    HttpService.get(`/api/DepartmentInLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_DEPARTMENTINLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getDepartmentInLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/DepartmentInLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addDepartmentInLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/DepartmentInLanguage/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getDepartmentInLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/DepartmentInLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editDepartmentInLanguage = ({ commit }, params) => {
    return HttpService.put('/api/DepartmentInLanguage/Update', params
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
    getDepartmentInLanguages,
    addDepartmentInLanguage,
    getDepartmentInLanguage,
    editDepartmentInLanguage,
    getDepartmentInLanguageAll
}
