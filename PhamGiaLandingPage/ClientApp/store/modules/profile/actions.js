import HttpService from '../../../plugins/http'

const updateProfile = ({commit}, data) => {
    return HttpService.post('/api/User/updateProfile', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e)
        })
};
const changePassword = ({commit}, data) => {
    return HttpService.post('/api/user/changepassword', data).then(response => {
        return response.data;
    }).catch(e => {
        alert('ex found:' + e)
    })
};
export default {
    updateProfile,
    changePassword
}
