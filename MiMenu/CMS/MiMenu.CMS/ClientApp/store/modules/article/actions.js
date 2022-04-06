import HttpService from "../../../plugins/http";

const getAllStatusOption = async () => {
    const response = await HttpService.get(`/api/Common/GetAllStatusOptions`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const getAllTag = async () => {
    const response = await HttpService.get(`/api/article/getalltag`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const getAllTypeOption = async () => {
    const response = await HttpService.get(`/api/Common/GetAllTypeOptions`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const getZoneArticle = async () => {
    const response = await HttpService.get(`/api/Article/GetZoneArticle`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const getProductAtArticle = async () => {
    const response = await HttpService.get(`/api/Article/GetProductAtArticle`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};


const getAllLanguageOption = async () => {
    const response = await HttpService.get(`/api/Common/GetAllLanguageOptions`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const getArticleById = async ({ commit }, data) => {
    console.log(data);

    let response = await HttpService.get(`/api/Article/GetById?Id=${data}`).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};


const publishArticle = async ({ commit }, data) => {
    console.log(data);
    const response = await HttpService.post(`/api/article/publish?Id=${data}`).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};


const unpublishArticle = ({ commit }, data) => {
    return HttpService.post(`/api/article/unpublish?id=${data}`)
        .then(response => {
            console.log(response.data);
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const deleteArticle = ({ commit }, data) => {
    return HttpService.delete(`/api/Article/Delete?id=${data}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const getArticles = ({ commit }, data) => {
    HttpService.get(`/api/Article/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}`).then(response => {
        commit("GET_ARTICLES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
};
const getArticle = ({ commit }, data) => {
    HttpService.get(`/api/Article/GetById?Id=${data}`).then(response => {
        commit("GET_ARTICLE", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
};
const getPageArticle = ({ commit }, data) => {
    HttpService.post(`/api/Article/GetPage`, data).then(response => {
        console.log(response.data);
        commit("GET_PAGE_ARTICLES", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
};

const addArticle = ({ commit }, data) => {
    return HttpService.post(`/api/Article/Add`,
        data
    ).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
};
const ExportExcelViewCount = ({ commit }, data) => {
    //debugger
    return HttpService.post(`/api/Article/ExportViewCount`,
        data
    ).then(response => {
        //debugger
        //console.log(response);
        return response;
    }).catch(e => {
        alert('ex found:' + e)
    })
};

const updateArticle = ({ commit }, data) => {
    return HttpService.put(`/api/Article/Update`,
        data
    ).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
};


const autoGenIndexing = ({ commit }, data) => {
    return HttpService.post(`/api/Article/genIndexing`,
        data
    ).then(response => {
        return response.data
    }).catch(e => {
        alert('ex found:' + e)
    })
};






export default {
    getArticles,
    getArticle,
    updateArticle,
    deleteArticle,
    getPageArticle,
    addArticle,
    getAllStatusOption,
    getZoneArticle,
    getAllTypeOption,
    getProductAtArticle,
    getAllLanguageOption,
    getArticleById,
    publishArticle,
    unpublishArticle,
    getAllTag,
    autoGenIndexing,
    ExportExcelViewCount
}
