import HttpService from '../../../plugins/http'

const getBannerAdss = ({ commit }, data) => {
    return HttpService.get(`/api/BannerAds/Get?tuKhoa=${data.keyword}`, {}).then(response => {
        commit("GET_BANNERS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getBannerCode = ({ commit }) => {
    return HttpService.get(`/api/BannerAds/GetAllCode`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getBannerByCode = ({ commit }, code) => {
    return HttpService.get(`/api/BannerAds/GetByCode?code=${code}`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getBannerAds = ({ commit }, id) => {
    return HttpService.get(`/api/BannerAds/GetById?Id=${id}`, {}).then(response => {
        return response.data;        
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const createBannerAds = ({ commit }, data) => {
    return HttpService.post(`/api/BannerAds/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const removeBannerAds = ({ commit }, key) => {
    return HttpService.delete(`/api/BannerAds/Delete?key=${key}`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getGuarantee = ({ commit }) => {
    return HttpService.get(`/api/BannerAds/GetGuarantee`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getBannerAdss,
    getBannerAds,
    createBannerAds,
    removeBannerAds,
    getBannerCode,
    getGuarantee,
    getBannerByCode
}
