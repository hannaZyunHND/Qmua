import actions from './actions'

const _locationstore = {
    state: {
        locations: [{}],
        location: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
         
        },
        GET_LOCATIONS: (state, payload) => {
            state.locations = payload;
        }
    },
    actions,
    getters: {
        locations: state => state.locations,
        location: state => state.location,
        
    }
}
export default _locationstore;
