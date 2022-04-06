import HttpService from '../../../plugins/http'
import { config } from 'vue-test-utils';

const uploadFile = ({ commit }, data) => {
    return HttpService.post('/api/UploadFileV2/UploadImage', data)
        .then(response => {
            return response.data;
        })
        .catch(e => {
            alert('ex found:' + e);
        })
}

export default {
    uploadFile
}
