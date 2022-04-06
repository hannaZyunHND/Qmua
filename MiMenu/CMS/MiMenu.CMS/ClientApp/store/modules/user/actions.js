import HttpService from '../../../plugins/http'

const getPageUser = async ({commit}, data) => {
    const response = await HttpService.get(`/api/user/getpage?pageIndex=${data.pageIndex}&pageSize=${data.pageSize}&keyword=${data.title}`).catch(e => {
        alert('ex found:' + e)
    });
    return response.data;
};
const addUser = async ({commit}, data) => {
    const response = await HttpService.post(`/api/user/add`,
        data
    ).catch(e => {
        alert('ex found:' + e)
    });
    console.log(response.data);
    return response.data;
};

const getById = async ({commit}, data) => {
    return HttpService.get(`/api/user/getbyid?id=${data}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const updateUser = async ({commit}, data) => {
    return HttpService.post('/api/User/updateProfile', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const getRoleName = async ({commit}, data) => {
    var response = await HttpService.get(`/api/user/getRoleNameByUserId?id=${data}`);
    return response.data;
};
const lockUser = async ({commit}, data) => {
    return HttpService.post(`/api/user/lockUser?id=${data}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};

const assignToRole = async ({commit}, data) => {
    return HttpService.put(`/api/user/assigntoroles?id=${data.id}&roleName=${data.roleName}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};
const getAllRole = async () => {
    var response = await HttpService.get('/api/role/getalloptions');
    return response.data;
};

export default {
    getPageUser,
    addUser,
    getById,
    updateUser,
    assignToRole,
    getAllRole,
    getRoleName,
    lockUser
}
