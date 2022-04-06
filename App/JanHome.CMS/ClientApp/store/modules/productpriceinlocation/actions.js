import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getProductInLocations = ({ commit }, data) => {
    return HttpService.get(`/api/Location/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDir=${data.sortDir}`, {
    }).then(response => {
        commit("GET_PRICEINLOCATIONS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getProductInLocationAll = ({ commit }) => {
    return HttpService.get(`/api/ProductPriceInLocation/GetAll`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getProductInLocation = ({ commit }, id) => {
    return HttpService.get(`/api/ProductPriceInLocation/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateProductInLocation = ({ commit }, data) => {
    return HttpService.post('/api/ProductPriceInLocation/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addProductInLocation = ({ commit }, params) => {
    return HttpService.post('/api/ProductPriceInLocation/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteProductInLocation = ({ commit }, data) => {
    return HttpService.delete(`/api/ProductPriceInLocation/Delete?id=${data.Id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getProductInLocations,
    getProductInLocationAll,
    getProductInLocation,
    updateProductInLocation,
    addProductInLocation,
    deleteProductInLocation,
}
