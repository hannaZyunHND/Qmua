import actions from './action'
const _propertylanguagestore = {
    state: {
        propertylanguages: [{}],
        propertylanguage: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
          
        },
        GET_PROPERTYLANGUAGES: (state, payload) => {
            state.propertylanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        propertylanguages: state => state.propertylanguages,
    }
}
export default _propertylanguagestore;
