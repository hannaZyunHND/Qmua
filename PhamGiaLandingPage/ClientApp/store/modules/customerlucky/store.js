import actions from './actions'

const _customerLucky = {
    state: {
        cslucky: []
       
    },
    mutations: {
       
        GET_ALL: (state, payload) => {
            state.cslucky = payload;
        },
    },
    actions,// cal action
    getters: {
        cslucky: state => state.cslucky,
       
    }
}
export default _customerLucky;
