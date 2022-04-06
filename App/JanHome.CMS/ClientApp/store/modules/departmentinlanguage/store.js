import actions from './action'
const _departmentinlanguagestore = {
    state: {
        departmentinlanguages: [{}],
        departmentinlanguage: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            
        },
        GET_DEPARTMENTINLANGUAGES: (state, payload) => {
            state.departmentinlanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        departmentinlanguages: state => state.departmentinlanguages,
    }
}
export default _departmentinlanguagestore;
