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
  "Authorization": "BEARER " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5MDk3MTAzNSwiZXhwIjoxNTkxMDU3NDM1LCJpYXQiOjE1OTA5NzEwMzV9.pEto3mN2cCC5O8sn-AjGJ6WC3f1efqhoNKh6lnjZJ1w",
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
