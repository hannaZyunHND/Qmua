import Vue from 'vue';
import alert from 'sweetalert2';

alert.setDefaults({
    width: '400px'
});

Vue.prototype.$alert = alert;
