import actions from './actions'
//import _ from 'lodash';
const _propertytstore = {
    
    state: {
        propertys: [{}],
        property: ''
    },
    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_PROPERTYS: (state, payload) => {
            state.propertys = payload;
        }
    },
    actions,// cal action
    getters: {
        propertys: state => state.propertys,
        property: state => state.property,
        //isLoading: state => state.isLoading
    }
}
export default _propertytstore;
