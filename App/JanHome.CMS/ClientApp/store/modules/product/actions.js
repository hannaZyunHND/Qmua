import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const getProducts = ({ commit }, data) => {
    return HttpService.post(`/api/Product/Search`, data).then(response => {
        commit("GET_PRODUCTS", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const addPromotionWithProduct = ({ commit }, data) => {
    return HttpService.post(`/api/Promotion/CreatePromotionWithProduct`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const getProduct = ({ commit }, id) => {
    return HttpService.get(`/api/Product/GetById?id=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getNameProduct = ({ commit }, id) => {
    return HttpService.get(`/api/Product/GetById?id=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getPriceByLocation = ({ commit }, id) => {
    return HttpService.get(`/api/Product/GetPriceByLocation?idProduct=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllRegion = ({ commit }, id) => {
    return HttpService.get(`/api/ProductInRegion/GetAllRegion`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getProductByRegion = ({ commit }, data) => {

    return HttpService.get(`/api/ProductInRegion/getProductByRegion?zoneId=${data.zoneId}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getAllTypeMaterial = ({ commit }) => {

    return HttpService.get(`/api/Common/GetAllTypeMaterial`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const addProductByRegion = ({ commit }, params) => {
    return HttpService.post('/api/ProductInRegion/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const getPropertyProduct = ({ commit }) => {
    return HttpService.get('/api/Product/GetPropertyProduct')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const getLanguageById = ({ commit }, id) => {
    return HttpService.get(`/api/Product/GetLanguageById?id=${id}`, {})
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const productUnitGet = ({ commit }, data) => {
    return HttpService.get('/api/Common/ProductUnitGet')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const updateProduct = ({ commit }, data) => {
    return HttpService.put('/api/Product/Update', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const addProduct = ({ commit }, params) => {
    return HttpService.post('/api/Product/Add', params)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}

const deleteProduct = ({ commit }, data) => {
    return HttpService.put(`/api/Product/Delete?id=${data.id}&status=${data.status}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const supportsProduct = ({ commit }, data) => {
    return HttpService.get('/api/Product/Supports')
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
}
const exportPriceInLocation = ({ commit }, data) => {
    return HttpService.post('/api/ImportExport/ExportProductPriceLocation', data).then(response => {
        return response;
        //return response;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const exportSpectifications = ({ commit }, data) => {
    return HttpService.post('/api/ImportExport/ExportSpectification', data).then(response => {
        return response;
        //return response;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const updateSort = ({ commit }, data) => {
    return HttpService.put(`/api/Product/UpdateSort?id=${data.id}&sortNew=${data.sortNew}`, {}).then(response => {
        return response.data;
        //return response;
    }).catch(e => {
        alert('ex found:' + e)
    })
}


export default {
    getProducts,
    getProduct,
    updateProduct,
    deleteProduct,
    addProduct,

    getPriceByLocation,
    productUnitGet,
    getAllRegion,
    getProductByRegion,
    addProductByRegion,
    supportsProduct,
    getPropertyProduct,
    getLanguageById,

    addPromotionWithProduct,
    getAllTypeMaterial,
    getNameProduct,
    exportPriceInLocation,
    exportSpectifications,
    updateSort
}
