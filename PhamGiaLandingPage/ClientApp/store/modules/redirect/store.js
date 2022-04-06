import actions from './actions'

const _store = {
    state: {
        colors: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {

        },
        GET_COLORS: (state, payload) => {
            state.colors = payload;
        }
    },
    actions,
    getters: {
        colors: state => state.colors,
    }
}
export default _store;
