import actions from './actions'

const _store = {
    state: {
        customers: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {
          
        },
        GET_CUSTOMERS: (state, payload) => {
            state.customers = payload;
        }
    },
    actions,
    getters: {
        customers: state => state.customers,
    }
}
export default _store;
