import HttpService from '../../../plugins/http'

const getCustomers = ({ commit }, data) => {
    return HttpService.post(`/api/Customer/Get`, data).then(response => {
        commit("GET_CUSTOMERS", { ...response.data })
        //return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const updateStatusCustomer = ({ commit }, data) => {
    return HttpService.put(`/api/Customer/UpdateStatus`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllTypeCustomer = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllTypeCustomer`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllSourceCustomer = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllSourceCustomer`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createCustomer = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Customer/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const removeCustomer = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Customer/Delete`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}


export default {
    getCustomers,
    updateStatusCustomer,
    getAllTypeCustomer,
    getAllSourceCustomer,
    createCustomer,
    removeCustomer

}
