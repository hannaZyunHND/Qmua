import actions from './actions'

const _store = {
    state: {
        bankinstallments: [],
        bankinstallment: {}
    },
    mutations: {
        SET_LOADING: (state, payload) => {

        },
        GET_INSTALLMENTS: (state, payload) => {
            state.bankinstallments = payload;
        }
    },
    actions,
    getters: {
        bankinstallments: state => state.bankinstallments,
        bankinstallment: state => state.bankinstallment
    }
}
export default _store;
