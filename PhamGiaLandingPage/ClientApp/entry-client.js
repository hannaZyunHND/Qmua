import { app,store } from './main.js';
import './assets/css/site.css'
import 'core-js/es6/promise'
import 'core-js/es6/array'

if (window.__INITIAL_STATE__) {
  // We initialize the store state with the data injected from the server
  store.replaceState(window.__INITIAL_STATE__);
}

app.$mount('#app')
