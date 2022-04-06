import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getOrders = ({ commit }, data) => {
    return HttpService.post(`/api/Orders/Get`, data).then(response => {
        console.log(response);
        commit("GET_ORDERS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getOrder = ({ commit }, id) => {
    return HttpService.get(`/api/Orders/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getOrderStatus = ({ commit }) => {
    return HttpService.get(`/api/Common/OrderStatusGet`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateOrderStatus = ({ commit }, data) => {
    return HttpService.put(`/api/Orders/UpdateOrderStatus`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const getAllTypeOrder = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllTypeOrder`, {
        }).then(response => {
            return response.data;
        }).catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getOrders,
    getOrder,
    getOrderStatus,
    updateOrderStatus,
    getAllTypeOrder
}
