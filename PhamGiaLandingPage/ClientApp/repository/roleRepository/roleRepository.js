import HttpService from '../../plugins/http'

const roleRepository = {
    assignPermissionRole(id,data) {
        return HttpService.post(`/api/permission/save-permissions?role=${id}`,data)
            .then(response => {
                console.log(response.data);
                return response.data;
            })
            .catch(e => {
                alert('ex found:' + e)
            })
    },
};


export default roleRepository;
