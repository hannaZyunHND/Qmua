import actions from './actions'
//import _ from 'lodash';
const _productstore = {
    state: {
  
        products: [{}],
        product: '',
        dataproductsearch: [{}],
    },
    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_PRODUCTS: (state, payload) => {
            state.products = payload;
        },
        SEARCH_PRODUCTS: (state, payload) => {
            state.dataproductsearch = payload.listData;
        }
    },
    actions,// cal action
    getters: {
        products: state => state.products,
        dataproductsearch: state => state.dataproductsearch,
        product: state => state.product,
        //isLoading: state => state.isLoading
    }


}
export default _productstore;
