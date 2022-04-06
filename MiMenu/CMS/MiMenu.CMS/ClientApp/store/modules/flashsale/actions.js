import HttpService from '../../../plugins/http'

const getFlashSales = ({ commit }, data) => {
    return HttpService.post(`/api/FlashSale/Get`, data).then(response => {
        return response.data;

    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getFlashSale = ({ commit }, Id) => {
    return HttpService.get(`/api/FlashSale/GetById?Id=${Id}`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createFlashSale = ({ commit }, data) => {
    debugger
    return HttpService.post(`/api/FlashSale/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateStatusFlashSale = ({ commit }, data) => {
    debugger
    return HttpService.put(`/api/FlashSale/UpdateTrangThai?id=${data.id}&status=${data.status}`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getFlashSale,
    getFlashSales,
    createFlashSale,
    updateStatusFlashSale,

}
