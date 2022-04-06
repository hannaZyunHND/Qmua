import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from './App.vue'
import BootstrapVue from 'bootstrap-vue'
import CoreuiVue from '@coreui/vue';


import Toast from "vue-toastification";
import Loading from 'vue-loading-overlay';
import vSelect from 'vue-select'
import CKEditor from 'ckeditor4-vue';


import Vuelidate from 'vuelidate';


Vue.use(Loading,
    {});



import "vue-toastification/dist/index.css";
import { router } from "./router";


Vue.component('v-select', vSelect);


const options = {
    position: "top-right",
    timeout: 2769,
    closeOnClick: true,
    pauseOnFocusLoss: true,
    pauseOnHover: true,
    draggable: true,
    draggablePercent: 0.1,
    hideCloseButton: false,
    hideProgressBar: false,
    icon: true,
    transition: "Vue-Toastification__fade",
    maxToasts: 8,
    newestOnTop: true
};



Vue.use(CoreuiVue);
Vue.use(Toast, options);
Vue.prototype.$http = axios;

Vue.use(BootstrapVue);
Vue.use(Vuex);

Vue.use(CKEditor);
Vue.use(Vuelidate);

sync(store, router);




const app = new Vue({
    store,
    router,
    ...App
});

export {
    app,
    router,
    store
}
