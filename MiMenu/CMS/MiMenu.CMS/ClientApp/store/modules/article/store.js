import actions from './actions'

const _articlestore = {
    state: {
        articless: [{}],
        article: '',
        totalArticles: 0
    },

    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_PAGE_ARTICLES: (state, payload) => {
            state.articless = payload;
        },
        GET_ARTICLES: (state, payload) => {
            state.articless = payload;
        },
        GET_ARTICLE: (state, payload) => {
            state.article = payload.data;
        },
        UPDATE_ARTICLE_CLONE: (state, payload) => {
            state.article = payload;
        },
        UPDATE_ARTICLE: (state, payload) => {
            state.isOR = payload.success;
        },
        Add_ARTICLE: (state, payload) => {
            state.isOR = payload.success;
        },
        Delete_ARTICLE: (state, payload) => {
            state.isOR = payload.data;
        },
    },
    actions,// cal action
    getters: {
        articless: state => state.articless,
        article: state => state.article,
       
        totalArticles: state => state.totalArticles,
        //isLoading: state => state.isLoading,
        isOR: state => state.isOR,
    }
};
export default _articlestore;
