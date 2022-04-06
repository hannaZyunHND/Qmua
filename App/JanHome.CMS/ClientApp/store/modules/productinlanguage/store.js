import actions from './action'

const _productinlanguagestore = {
    state: {
        productinlanguages: [{}],
        productinlanguage: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
           
        },
        GET_PRODUCTINLANGUAGES: (state, payload) => {

            state.productinlanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        productinlanguages: state => state.productinlanguages,

    }
}
export default _productinlanguagestore;
