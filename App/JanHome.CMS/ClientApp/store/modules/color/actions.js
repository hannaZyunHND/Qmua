import HttpService from '../../../plugins/http'

const getColors = ({ commit }, data) => {
    return HttpService.post(`/api/Colors/Get`, data).then(response => {
        return response.data;
        //return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getAllColors = ({ commit }) => {
    return HttpService.get(`/api/Colors/GetAll`).then(response => {
        return response.data;
        
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createColors = ({ commit }, data) => {
    debugger
    return HttpService.post(`/api/Colors/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const removeColors = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Colors/Delete`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getColors,
    createColors,
    removeColors,
    getAllColors
}
