import HttpService from '../../plugins/http'


const productSpecificationTemplateRepository = {
    async getProductSpecificationTemplate(data) {
        const response = await HttpService.get(`/api/productspecificationtemplate/get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.title}`, {}).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response);
        return response.data;
    },

    async addProductSpecificationTemplate(data) {
        const response = await HttpService.post(`/api/productspecificationtemplate/Add`,
            data
        ).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response.data);
        return response.data;
    },

    async updateProductSpecificationTemplate(data) {
        const response = await HttpService.put(`/api/productspecificationtemplate/Update`,
            data
        ).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response.data);
        return response.data;
    },

    async getProductSpecificationTemplateById(id) {
        const response = await HttpService.get(`/api/productspecificationtemplate/GetById?id=${id}`
        ).catch(e => {
            alert('ex found:' + e)
        });
        return response.data;
    },

    deleteProductSpecificationTemplate(data) {
        return HttpService.post(`/api/ProductSpecificationTemplate/unpublish?id=${data}`)
            .then(response => {
                console.log(response.data);
                return response.data;
            })
            .catch(e => {
                alert('ex found:' + e)
            })
    },
    async getAllLanguageOption() {
        const response = await HttpService.get(`/api/Common/GetAllLanguageOptions`).catch(e => {
            alert('ex found:' + e)
        });
        return response.data;
    },

    async getZoneArticle() {
        const response = await HttpService.get(`/api/Article/GetZoneArticle`).catch(e => {
            alert('ex found:' + e)
        });
        return response.data;
    }
};

export default productSpecificationTemplateRepository;
