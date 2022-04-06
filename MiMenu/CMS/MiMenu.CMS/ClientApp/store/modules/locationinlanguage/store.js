import actions from './action'
const _locationinlanguagestore = {
    state: {
        locationinlanguages: [{}],
        locationinlanguage: ''
    },

    mutations: {
        SET_LOADING: (state, payload) => {
            
        },
        GET_LOCATIONSLANGUAGES: (state, payload) => {
            state.locationinlanguages = payload;

        },

    },
    actions,// cal action
    getters: {
        locationinlanguages: state => state.locationinlanguages,
    }
}
export default _locationinlanguagestore;
