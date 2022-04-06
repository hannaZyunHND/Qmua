import HttpService from '../../../plugins/http'

const getComments = ({ commit }, data) => {
    return HttpService.post(`/api/Comment/Get`, data).then(response => {
        commit("GET_COMMENTS", { ...response.data })
        //return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const updateStatusComment = ({ commit }, data) => {
    return HttpService.put(`/api/Comment/UpdateStatus`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllTypeComment = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllTypeComment`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllStatusComment = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllStatusComment`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createComment = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Comment/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const removeComment = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Comment/Delete`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
export default {
    getComments,
    updateStatusComment,
    getAllTypeComment,
    getAllStatusComment,
    createComment,
    removeComment

}
