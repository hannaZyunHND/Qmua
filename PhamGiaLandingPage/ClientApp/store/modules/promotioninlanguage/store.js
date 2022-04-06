import actions from './action'

const _promotioninlanguagestore = {
    state: {
        promotioninlanguages: [{}],
        promotioninlanguage: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            state.isLoading = payload;
        },
        GET_PROMOTIONINLANGUAGES: (state, payload) => {
            state.promotioninlanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        promotioninlanguages: state => state.promotioninlanguages,
    }
}
export default _promotioninlanguagestore;
