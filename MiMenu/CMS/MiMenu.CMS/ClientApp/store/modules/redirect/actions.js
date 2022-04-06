import HttpService from '../../../plugins/http'

const getRedirects = ({ commit }, data) => {
    return HttpService.post(`/api/Redirect/Get`, data).then(response => {
        return response.data;
        
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const createRedirects = ({ commit }, data) => {
    debugger
    return HttpService.post(`/api/Redirect/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const removeRedirects = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Redirect/Delete`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getRedirects,
    createRedirects,
    removeRedirects,
   
}
