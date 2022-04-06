import HttpService from "../../../plugins/http";

const getCouponsChild = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/GetAllPaging`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const GetCouponsChildId = ({ commit }, id) => {
    return HttpService.get(`/api/CouponChild/GetById?ma=${id}`, {}).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const GetByCouponsChildParrentId = ({ commit }, pid) => {
    return HttpService.get(`/api/CouponChild/GetByParrentId?parrentId=${pid}`, {}).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const PublishCouponChild = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/ChangeStatus`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const MergeListCoupon = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/MergeList`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const MergeCouponChild = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/Merge`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const DeleteCouponChild = ({ commit }, data) => {
    return HttpService.delete(`/api/CouponChild/Delete`, data.ma).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const addListVoucherByProduct = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/AddByListProduct`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const addListVoucherByZone = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/AddByListZone`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const readExcel = ({ commit }, data) => {
    return HttpService.post(`/api/CouponChild/ReadExcel`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


export default {
    getCouponsChild,
    MergeListCoupon,
    MergeCouponChild,
    DeleteCouponChild,
    GetCouponsChildId,
    PublishCouponChild,
    GetByCouponsChildParrentId,
    addListVoucherByZone,
    addListVoucherByProduct,
    readExcel
}
