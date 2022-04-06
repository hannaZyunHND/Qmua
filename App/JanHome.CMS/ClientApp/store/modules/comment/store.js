import actions from './actions'

const _store = {
    state: {
        comments: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {
            
        },
        GET_COMMENTS: (state, payload) => {
            state.comments = payload;
        }
    },
    actions,
    getters: {
        comments: state => state.comments,
    }
}
export default _store;
