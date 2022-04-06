import actions from './actions'

const _priceinlocationstore = {
    state: {
        priceinlocations: [{}],
        priceinlocation: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            
        },
        GET_PRICEINLOCATIONS: (state, payload) => {
            state.priceinlocations = payload;
        },
        GET_PRICEINLOCATION: (state, payload) => {
            state.priceinlocation = payload.Data;
        },
       
    },
    actions,
    getters: {
        priceinlocations: state => state.priceinlocations,
        priceinlocation: state => state.priceinlocation,
       
    }
}
export default _priceinlocationstore;
