import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';
import debounce from '../../../services/debounce';

const getConfigInLanguages = ({ commit }, data) => {
    HttpService.get(`/api/ConfigInLanguage/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_CONFIGINLANGUAGES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getConfigInLanguageAll = ({ commit }, data) => {
    return HttpService.get(`/api/ConfigInLanguage/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addConfigInLanguage = ({ commit }, params) => {
    return HttpService.post(`/api/ConfigInLanguage/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getConfigInLanguage = ({ commit }, id) => {
    return HttpService.get(`/api/ConfigInLanguage/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editConfigInLanguage = ({ commit }, params) => {
    return HttpService.put('/api/ConfigInLanguage/Update', params
    ).then(response => {
        return response.data;
    })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const editConfigInLanguages = ({ commit }, params) => {
    return HttpService.put('/api/ConfigInLanguage/Updates', params
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
    getConfigInLanguages,
    addConfigInLanguage,
    getConfigInLanguage,
    editConfigInLanguage,
    getConfigInLanguageAll,
    editConfigInLanguages
}
