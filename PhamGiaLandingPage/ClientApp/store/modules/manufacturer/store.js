import actions from './actions'

const _manufacturerstore = {
    state: {
        manufacturers: [{}],
        manufacturer: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
           
        },
        GET_MANUFACTURERSS: (state, payload) => {
            state.manufacturers = payload;
        },
        GET_MANUFACTURERS: (state, payload) => {
            state.manufacturer = payload.Data;
        },
       
    },
    actions,
    getters: {
        manufacturers: state => state.manufacturers,
        manufacturer: state => state.manufacturer,
 
    }
}
export default _manufacturerstore;
