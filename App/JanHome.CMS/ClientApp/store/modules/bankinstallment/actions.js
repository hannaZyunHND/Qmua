import HttpService from '../../../plugins/http'

const getBankInstallments = ({ commit }, data) => {
    return HttpService.get(`/api/BankInstallment/Get?tuKhoa=${data.keyword}`, {}).then(response => {
        commit("GET_INSTALLMENTS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAll = ({ commit }) => {
    return HttpService.get(`/api/BankInstallment/GetAll`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getBankInstallment = ({ commit }, id) => {
    return HttpService.get(`/api/BankInstallment/GetById?Id=${id}`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const createBankInstallment = ({ commit }, data) => {
    return HttpService.post(`/api/BankInstallment/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const removeBankInstallment = ({ commit }, id) => {
    return HttpService.delete(`/api/BankInstallment/Delete?id=${id}`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getCardType = ({ commit }) => {
    return HttpService.get(`/api/BankInstallment/GetCardType`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getBankInstallments,
    getBankInstallment,
    createBankInstallment,
    removeBankInstallment,
    getAll,
    getCardType
}
