import Vue from 'vue'
import Vuex from 'vuex'
 import modules from './modules'
//import product from './../store/modules/product'
Vue.use(Vuex)
const store= new Vuex.Store({
    strict: process.env.NODE_ENV !== 'production',
    //namespaced: true,
     modules
   // product
})
export default store;

// export function createStore () {
//     return new Vuex.Store({
//       modules: {
//     //    strict: process.env.NODE_ENV !== 'production',
//     modules
//       }
//     })
//   }
