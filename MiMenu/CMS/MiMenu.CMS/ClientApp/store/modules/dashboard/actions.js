import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getDashBoard1 = ({ commit }) => {
    return HttpService.get(`/api/DashBoard/GetDataDashBoard1`).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getDashBoard2 = ({ commit }, params) => {
    return HttpService.get(`/api/DashBoard/GetDataDashBoard2?month=${params.month}&year=${params.year}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getDashBoard3 = ({ commit }) => {
    return HttpService.get(`/api/DashBoard/GetDataDashBoard3`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getCommentData = ({ commit }) => {
    return HttpService.get(`/api/DashBoard/GetCommentData`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getDashBoard1,
    getDashBoard2,
    getDashBoard3,
    getCommentData
}
