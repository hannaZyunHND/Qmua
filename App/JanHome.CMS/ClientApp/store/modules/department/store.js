import actions from './actions'

const _departmentstore = {
    state: {
        departments: [{}],
        department: ''
    },

    mutations: {
        //SET_LOADING: (state, payload) => {
        //    state.isLoading = payload;
        //},
        GET_DEPARTMENTS: (state, payload) => {
            state.departments = payload;
        }
    },
    actions,
    getters: {
        departments: state => state.departments,
        department: state => state.department,
        
    }
}
export default _departmentstore;
