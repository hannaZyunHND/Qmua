import HttpService from '../../../plugins/http'

const fmRemove = ({ commit }, id) => {
    return HttpService.delete(`/api/FileUploadV2/Delete?id=${id}`)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}
const fmFileUpload = ({ commit }, data) => {
    return HttpService.post('/api/FileUploadV2/UploadImage', data, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}
const fmFileUpload_2 = ({ commit }, data) => {
    return HttpService.post('/api/FileUploadV2/UploadFile', data, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}
const fmFileGetAll = ({ commit }, data) => {
    return HttpService.get(`/api/FileUploadV2/getall?take=${data.pageSize}&pageIndex=${data.pageIndex}&keyword=${data.keyword}`, {})
        .then(response => {
            let listFiles = {
                files: response.data.data,
                totals: response.data.totalRow
            }
           
            commit("GET_ALL_FILE", listFiles);
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}
const fmFileUploadExcel = ({ commit }, data) => {
    return HttpService.post('/api/FileUploadV2/UploadExcel', data, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}

const fmFileUploadExcel_Spectification = ({ commit }, data) => {
    return HttpService.post('/api/FileUploadV2/UploadExcel_Spectification', data, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}

export default {
    fmRemove,
    fmFileUpload,
    fmFileGetAll,
    fmFileUploadExcel,
    fmFileUploadExcel_Spectification,
    fmFileUpload_2
}
