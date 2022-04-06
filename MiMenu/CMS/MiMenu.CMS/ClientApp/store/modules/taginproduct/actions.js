import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getTagInProducts = ({ commit }, data) => {
    return HttpService.get(`/api/TagInProduct/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {
    }).then(response => {
        commit("GET_TAGINPRODUCTS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getTagInProductAll = ({ commit }) => {
    return HttpService.get(`/api/TagInProduct/GetAll`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getTagInProduct = ({ commit }, id) => {
    return HttpService.get(`/api/TagInProduct/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateTagInProduct = ({ commit }, data) => {
    return HttpService.post('/api/TagInProduct/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addTagInProduct = ({ commit }, params) => {
    return HttpService.post('/api/TagInProduct/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteTagInProduct = ({ commit }, data) => {
    return HttpService.delete(`/api/TagInProduct/Delete?id=${data.Id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getTagInProducts,
    getTagInProductAll,
    getTagInProduct,
    updateTagInProduct,
    addTagInProduct,
    deleteTagInProduct
}
