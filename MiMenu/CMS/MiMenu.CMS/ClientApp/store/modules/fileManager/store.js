import actions from './actions'
const _uploadfilestore = {
    state: {
        fileName: '',
        files: Object
    },
    mutations: {
       
        GET_FILENAME: (state, payload) => {
            state.fileName = payload;
        },
        GET_ALL_FILE: (state, payload) => {
            
            state.files = payload;
        }
    },
    actions,
    getters: {
        fileName: state => state.fileName,
        files: state => state.files
    }


}
export default _uploadfilestore;
