import actions from './actions'

const _taginproductstore = {
    state: {
        taginproducts: [{}],
        taginproduct: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            state.isLoading = payload;
        },
        GET_TAGINPRODUCTS: (state, payload) => {
            state.taginproducts = payload;
        },
        GET_TAGINPRODUCT: (state, payload) => {
            state.taginproduct = payload.Data;
        },
        UPDATE_TAGINPRODUCT: (state, payload) => {
            state.isOR = payload.data;
        },
        ADD_TAGINPRODUCT: (state, payload) => {
            state.isOR = payload.data;
        },
        DELETE_TAGINPRODUCT: (state, payload) => {
            state.isOR = payload.data;
        },
    },
    actions,
    getters: {
        taginproducts: state => state.taginproducts,
        taginproduct: state => state.taginproduct,
      
    }
}
export default _taginproductstore;
