import HttpService from '../../../plugins/http'


const getPageRole = async ({commit}, data) => {
    const response = await HttpService.get(`/api/role/getallpaging?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.title}`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};

const addRole = async ({commit}, data) => {
    const response = await HttpService.post(`/api/role/create`,
        data
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};

const getRoleById = ({commit}, data) => {
    return HttpService.get(`/api/role/getbyid?id=${data}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const updateRole = async ({commit}, data) => {
    const response = await HttpService.put(`/api/role/update?id=${data.Id}`,
        data
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};

const getListFunctions=async()=>{
    const response = await HttpService.get(`/api/role/getallfunctions`
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};

const getListActions=async ()=>{
    const response = await HttpService.get(`/api/role/getallactions`
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};

const deleteRole=async ({commit}, data)=>{
    const response = await HttpService.post(`/api/role/delete?id=${data}`
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
}


export default {
    getPageRole,
    addRole,
    getRoleById,
    updateRole,
    getListFunctions,
    getListActions,
    deleteRole
}
