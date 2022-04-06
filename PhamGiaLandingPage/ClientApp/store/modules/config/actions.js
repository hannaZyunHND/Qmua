import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getConfigs = ({ commit }, data) => {
    return HttpService.get(`/api/Config/GetAll?languageCode=${data.languageCode}`, {
    }).then(response => {
        //debugger-
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getConfig = ({ commit }, configGroupKey) => {
    return HttpService.get(`/api/Config/GetById?id=${configGroupKey}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateConfig = ({ commit }, data) => {
    return HttpService.put('/api/Config/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addConfig = ({ commit }, params) => {
    return HttpService.post('/api/Config/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteConfig = ({ commit }, data) => {
    return HttpService.delete(`/api/Config/Delete?id=${data.configGroupKey}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const configTypeValueGet = ({ commit }, data) => {
    return HttpService.get('/api/Common/ConfigTypeValueGet')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

export default {
    getConfigs,
    getConfig,
    updateConfig,
    deleteConfig,
    addConfig,
    configTypeValueGet,
    //uploadExcelProductPriceInLocation
}
