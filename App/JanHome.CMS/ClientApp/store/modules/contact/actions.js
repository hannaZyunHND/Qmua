import HttpService from '../../../plugins/http'

const getContacts = ({ commit }, data) => {
    return HttpService.post(`/api/Contact/Get`, data).then(response => {
        commit("GET_CONTACTS", { ...response.data })
        //return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const updateStatusContact = ({ commit }, data) => {
    return HttpService.put(`/api/Contact/UpdateStatus`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateNoteContact = ({ commit }, data) => {
    return HttpService.put(`/api/Contact/UpdateNote`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllTypeContact = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllTypeContact`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllStatusContact = ({ commit }) => {
    return HttpService.get(`/api/Common/GetAllStatusContact`, {}).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createContact = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Contact/Add`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const removeContact = ({ commit }, data) => {
    //debugger-
    return HttpService.post(`/api/Contact/Delete`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    getContacts,
    updateStatusContact,
    getAllTypeContact,
    getAllStatusContact,
    createContact,
    removeContact,
    updateNoteContact
}
