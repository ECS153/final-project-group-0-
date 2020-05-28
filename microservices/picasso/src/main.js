import Vue from "vue";
import App from "./App.vue";
import Axios from "axios";
import router from "./router";

import "@/assets/css/global/global.css";
import "@/assets/css/global/reset.css";
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-default.css';
////////////////////////////////
/////// GLOBAL VARIABLES ///////
////////////////////////////////
Vue.prototype.$http = Axios;
Vue.prototype.$apiHeaders = {
  "Authorization": "BEARER " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5MDYzMzgwNCwiZXhwIjoxNTkwNzIwMjA0LCJpYXQiOjE1OTA2MzM4MDR9.NG70KVdlDxSY_tKubcIxv954o-Gp-OdDNY-H6yxjF0U",
  "Content-Type": "application/json"
},


////////////////////////////////
////// CONFIGURE SERVICES //////
////////////////////////////////
Vue.config.productionTip = false;
Vue.config.errorHandler = (err) => {
  this.err = err;
}
Vue.use(VueToast);



////////////////////////////////
////// START VUE INSTANCE //////
////////////////////////////////
new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
