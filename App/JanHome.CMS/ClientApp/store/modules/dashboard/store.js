import actions from './actions'

const _dashboardstore = {
    state: {
        dashboards: [{}],
        dashboard: ''
    },

    mutations: {
        GET_DASHBOARD1: (state, payload) => {
            state.dashboards = payload;
        }
    },
    actions,
    getters: {
        dashboards: state => state.dashboards,
        dashboard: state => state.dashboard,
        
    }
}
export default _dashboardstore;
