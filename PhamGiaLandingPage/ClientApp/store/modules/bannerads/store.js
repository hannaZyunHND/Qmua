import actions from './actions'

const _store = {
    state: {
        banneradss: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {

        },
        GET_BANNERS: (state, payload) => {
            state.banneradss = payload;
        }
    },
    actions,
    getters: {
        banneradss: state => state.banneradss,
    }
}
export default _store;
