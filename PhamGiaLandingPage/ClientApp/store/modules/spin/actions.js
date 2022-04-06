import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';



const getAll = ({ commit }) => {
    return HttpService.get(`/api/LuckySpin/getall`, {
    }).then(response => {
      commit("GET_ALL", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getObj = ({ commit }, id) => {
    return HttpService.get(`/api/LuckySpin/get?id=${id}`, {
    }).then(response => {
        return response.data;
        //commit("GET_ADS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const addSpin = ({ commit }, params) => {
    return HttpService.post('/api/LuckySpin/add', params, {
        headers: {
        //    'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const editSpin = ({ commit }, params) => {
    let config = {

    };
    return HttpService.put('/api/LuckySpin/Update', params, {
        headers: {
         //   'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const removeSpin = ({ commit }, id) => {
    return HttpService.delete(`/api/LuckySpin/Delete?id=${id}`)
        .then(response => {

            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getAll,
    getObj,
    addSpin,
    editSpin,
    removeSpin

}
