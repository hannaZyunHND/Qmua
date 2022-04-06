import HttpService from '../../../plugins/http'



const getZones = ({ commit }, loai) => {
    return HttpService.get(`/api/Zone/GetZones?loai=${loai}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getZone = ({ commit }, id) => {
    return HttpService.get(`/api/Zone/GetById?id=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getAllZone = ({ commit }, data) => {
    console.log(data);
    return HttpService.get(`/api/Zone/GetAll?tukhoa=${data.tuKhoa}&languageCode=${data.languageCode}&loai=${data.type}&trangThai=${data.status}`, {
    }).then(response => {
        console.log(response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}



const updateZone = ({ commit }, data) => {
    //debugger-
    return HttpService.put('/api/Zone/Update', data)
        .then(response => {
            //debugger-
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const updateSortZone = ({ commit }, data) => {
    return HttpService.put(`/api/Zone/UpdateSort?`, data)
        .then(response => {
            //debugger-
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}


const addZone = ({ commit }, params) => {

    return HttpService.post('/api/Zone/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const mergeZoneLang = ({ commit }, params) => {

    return HttpService.post('/api/Zone/MergeLang', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const deleteZone = ({ commit }, data) => {
    return HttpService.delete(`/api/Zone/Delete?id=${data.id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const supportsZone = ({ commit }, data) => {
    //debugger-
    return HttpService.get('/api/Zone/Supports')
        .then(response => {

            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const updateShowLayout = ({ commit }, data) => {
    return HttpService.put(`/api/Zone/UpdateShowLayout`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const updateShowSearch  = ({ commit }, data) => {
    return HttpService.put(`/api/Zone/UpdateShowSearch`, data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}



export default {
    getZones,
    getZone,
    updateZone,
    deleteZone,
    addZone,
    getAllZone,
    mergeZoneLang,
    supportsZone,
    updateSortZone,
    updateShowLayout,
    updateShowSearch
}
