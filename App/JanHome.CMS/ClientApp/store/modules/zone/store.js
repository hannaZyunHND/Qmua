import actions from './actions'

const _zonestore = {
    state: {
        zones: [{}],
        zone: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
           
        },
        GET_ZONES: (state, payload) => {
            state.zones = payload;
        },
        GET_ZONE: (state, payload) => {
            state.zone = payload.Data;
        },
       
    },
    actions,
    getters: {
        zones: state => state.zones,
        zone: state => state.zone,
       
    }
}
export default _zonestore;
