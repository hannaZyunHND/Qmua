import actions from './action'

const _articlestore = {
    state: {
      
        tags: [{}],
        tag: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            state.isLoading = payload;
        },
        GET_TAGS: (state, payload) => {

            state.tags = payload;

        },

    },
    actions,// cal action
    getters: {
        //adss: state => state.adss,
        tags: state => state.tags,
        //total: state => state.total,

    }
}
export default _articlestore;
