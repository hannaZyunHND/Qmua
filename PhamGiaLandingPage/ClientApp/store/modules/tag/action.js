import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getTags = ({ commit }, data) => {
    HttpService.get(`/api/Tag/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.keyword}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {

    }).then(response => {

        commit("GET_TAGS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getTagAll = ({ commit }, data) => {
    return HttpService.get(`/api/Tag/GetAll`, {

    }).then(response => {
        return response.data 
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addTag = ({ commit }, params) => {
    //console.log(params);
    return HttpService.post(`/api/Tag/Add`, params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getTag = ({ commit }, id) => {
    return HttpService.get(`/api/Tag/GetById?id=${id}`, {
    }).then(response => {

        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const editTag = ({ commit }, params) => {
    return HttpService.put('/api/Tag/Update', params
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
    getTags,
    addTag,
    getTag,
    editTag,
    getTagAll

}
