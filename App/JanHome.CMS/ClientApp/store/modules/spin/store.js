import actions from './actions'

const _articlestore = {
    state: {
        data: [],
        obj: { }
    },
    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_ALL: (state, payload) => {
            state.data = payload;
        },
        GET_ONE: (state, payload) => {
            state.obj = payload;
        },
        GET_ADSS: (state, payload) => {
            state.adss = payload;
        },
    },
    actions,// cal action
    getters: {
        data: state => state.data,
        obj: state => state.obj,
       // ads: state => state.ads,
    }
}
export default _articlestore;
