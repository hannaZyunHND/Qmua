import HttpService from '../../plugins/http'


const manufacturersRepository = {
    async getPageManufacturers(data) {
        const response = await HttpService.get(`/api/Manufacturer/Get?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.title}`, {}).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response);
        return response.data;
    },

    async addManufacturers(data) {
        const response = await HttpService.post(`/api/Manufacturer/Add`,
            data
        ).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response.data);
        return response.data;
    },

    async updateManufacturers(data) {
        const response = await HttpService.put(`/api/Manufacturer/Update`,
            data
        ).catch(e => {
            alert('ex found:' + e)
        });
        console.log(response.data);
        return response.data;
    },

    async getManufacturersById(id) {
        const response = await HttpService.get(`/api/Manufacturer/GetById?id=${id}`
        ).catch(e => {
            alert('ex found:' + e)
        });
        return response.data;
    },
    deleteManufacturers(data) {
        return HttpService.post(`/api/Manufacturer/UnPublish?id=${data}`)
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
};

export default manufacturersRepository;
