import actions from './actions'

const _languagestore = {
    state: {
        languages: [{}],
        language: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
           
        },
        GET_LANGUAGES: (state, payload) => {
            state.languages = payload;
        },
        GET_LANGUAGE: (state, payload) => {
            state.language = payload.Data;
        },
       
    },
    actions,
    getters: {
        languages: state => state.languages,
        language: state => state.language,
       
    }
}
export default _languagestore;
