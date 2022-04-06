import HttpService from "../../../plugins/http";

const getCoupons = ({ commit }, data) => {
    return HttpService.get(`/api/Coupon/GetAllPaging?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDir=${data.sortDir}&keyword=${data.keyword}`, {
    }).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getCouponById = ({ commit }, id) => {
    return HttpService.get(`/api/Coupon/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getAllCoupon = ({ commit }, id) => {
    return HttpService.get(`/api/Coupon/GetAll`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const unPublishCoupon = ({ commit }, item) => {
    return HttpService.get(`/api/Coupon/UnPublish?id=${item.id}&type=${item.type}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateCoupon = ({ commit }, data) => {
    return HttpService.put('/api/Coupon/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const addCoupon = ({ commit }, params) => {
    return HttpService.post('/api/Coupon/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const getProductInCoupon = ({ commit }, ma) => {
    return HttpService.get(`/api/Coupon/GetProductInCounpon?id=${ma}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const addProductInCoupon = ({ commit }, data) => {
    return HttpService.post(`/api/Coupon/AddProductInCounpon`, data)
        .then(response => {

            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
export default {
    getCoupons,
    getCouponById,
    updateCoupon,
    addCoupon,
    unPublishCoupon,
    getAllCoupon,
    getProductInCoupon,
    addProductInCoupon
}
