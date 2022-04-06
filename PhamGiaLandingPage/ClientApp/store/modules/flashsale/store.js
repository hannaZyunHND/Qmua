import actions from './actions'

const _store = {
    state: {
        flashsales: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {

        },
        GET_COLORS: (state, payload) => {
            state.flashsales = payload;
        }
    },
    actions,
    getters: {
        flashsales: state => state.flashsales,
    }
}
export default _store;
