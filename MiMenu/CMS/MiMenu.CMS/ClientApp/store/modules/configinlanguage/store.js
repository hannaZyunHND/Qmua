import actions from './action'
const _configinlanguagestore = {

    state: {
        configinlanguages: [{}],
        configinlanguage: ''
    },

    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_CONFIGINLANGUAGES: (state, payload) => {
            state.configinlanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        configinlanguages: state => state.configinlanguages,
    }
}
export default _configinlanguagestore;
