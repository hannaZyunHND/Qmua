import HttpService from '../../../plugins/http'
const customerLuckyGetAll = ({ commit }, data) => {
    HttpService.get(`/api/CustomLucky/getall?phone=${data.keyword}&pageIndex=${data.pageIndex}&pageSize=${data.pageSize}`, {

    }).then(response => {
        console.log(response);
        commit("GET_ALL", { ...response.data })
    }).catch(e => {
        alert('ex found:' + e)
    })
}

export default {
    customerLuckyGetAll
    
}
