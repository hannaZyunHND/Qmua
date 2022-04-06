import actions from './actions'

const _articlestore = {
    state: {
        adss: [{}],
        ads: '',
    },
    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_ADSS: (state, payload) => {
            state.adss = payload;
        },
    },
    actions,// cal action
    getters: {
        adss: state => state.adss,
        ads: state => state.ads,
    }
}
export default _articlestore;
