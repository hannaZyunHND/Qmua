import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const updateLocations = ({ commit }, data) => {
    return HttpService.post(`/api/ProductPriceInLocation/Add`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addCombo = ({ commit }, data) => {
    return HttpService.post(`/api/ComBoProduct/Add`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getListPromotionByProductId = ({ commit }, id) => {
    return HttpService.get(`/api/Promotion/GetPromotionByProductId?productId=${id}`, {
    }).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getPriceByLocations = ({ commit }, id) => {
    return HttpService.get(`/api/ProductPriceInLocation/GetPriceByLocation?idProduct=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const createCommentFake = ({ commit }, data) => {
    return HttpService.post(`/api/Comment/FakeComment`, data).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}



const getProductForCombo = ({ commit }, data) => {
    return HttpService.post(`/api/ComBoProduct/SearchAll`, data).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getItemAllForCombo = ({ commit }, data) => {
    return HttpService.post(`/api/ComBoProduct/SearchAll`, data).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const getProductByZone = ({ commit }, data) => {
    return HttpService.get(`/api/ProductInZone/Search?keyword=${data.keyword}&idZone=${data.idZone}&pageSize=${data.pageSize}&pageIndex=${data.pageIndex}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}
const addProductHotInZone = ({ commit }, data) => {
    return HttpService.put(`/api/ProductInZone/Update`, data).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const getListPromotion = ({ commit }, data) => {
    return HttpService.post(`/api/Promotion/GetAllPaging`, data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getPromotionByProducts = ({ commit }, id) => {
    return HttpService.get(`/api/ProductInPromotion/GetByProductId?productId=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}


const getSpecificationByProducts = ({ commit }, id) => {
    return HttpService.get(`/api/ProductSpecifications/GetByProductId?productId=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getComboById = ({ commit }, id) => {
    return HttpService.get(`/api/ComBoProduct/GetByProductId?productId=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const getComboSameById = ({ commit }, id) => {
    return HttpService.get(`/api/ComBoProduct/GetSameByProductId?productId=${id}`, {
    }).then(response => {
        //console.log('ProductById'+id+response.data);
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const addPromotions = ({ commit }, data) => {
    return HttpService.post(`/api/ProductInPromotion/Add`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}

const addSpecifications = ({ commit }, data) => {
    return HttpService.post(`/api/ProductSpecifications/Add`, data).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    updateLocations,
    getProductForCombo,
    addCombo,
    getComboById,
    getComboSameById,
    getPriceByLocations,
    getPromotionByProducts,
    addPromotions,
    getSpecificationByProducts,
    addSpecifications,
    getListPromotion,
    getListPromotionByProductId,
    getProductByZone,
    addProductHotInZone,
    getItemAllForCombo,
    createCommentFake

}
