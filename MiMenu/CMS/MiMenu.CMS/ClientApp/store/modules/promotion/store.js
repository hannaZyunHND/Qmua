import actions from './actions'
//import _ from 'lodash';
const _promotiontstore = {
    state: {
        promotions: [{}],
        promotion: ''
    },
    mutations: {
        SET_LOADING: (state, payload) => {
            state.isLoading = payload;
        },
        GET_PROMOTIONS: (state, payload) => {
            state.promotions = payload;
        }
    },
    actions,// cal action
    getters: {
        promotions: state => state.promotions,
        promotion: state => state.promotion,
        isLoading: state => state.isLoading
    }
}
export default _promotiontstore;
