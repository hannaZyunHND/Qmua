import actions from './actions'

const _store = {
    state: {
        contacts: [],

    },
    mutations: {
        SET_LOADING: (state, payload) => {
            
        },
        GET_CONTACTS: (state, payload) => {
            state.contacts = payload;
        }
    },
    actions,
    getters: {
        contacts: state => state.contacts,
    }
}
export default _store;
