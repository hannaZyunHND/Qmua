import actions from './actions'
const _orderstore = {
    state: {
        orders: [{}],
        order: '',
    },
    mutations: {
        GET_ORDERS: (state, payload) => {
            state.orders = payload;
        }
    },
    actions,// cal action
    getters: {
        orders: state => state.orders,
        order: state => state.order,

    }


}
export default _orderstore;
