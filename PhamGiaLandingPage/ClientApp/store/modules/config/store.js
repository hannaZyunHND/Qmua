import actions from './actions'

const _configstore = {
    state: {
        configs: [{}],
        config: ''
    },

    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_CONFIGS: (state, payload) => {
            state.configs = payload;
        }
    },
    actions,
    getters: {
        configs: state => state.configs,
        config: state => state.config,
      
    }
}
export default _configstore;
