import Vue from "vue";
import App from "./App.vue";
import Axios from "axios"
import router from "./router";

Vue.config.productionTip = false;
Vue.prototype.$loginToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5MDU1NDQwMSwiZXhwIjoxNTkwNjQwODAxLCJpYXQiOjE1OTA1NTQ0MDF9.Bgot7EIKjUk3hHsAdqL1GCMJeE9ezmRIxMSMvqmgEsM";
Vue.prototype.$http = Axios;
import "@/assets/css/global.css"
import "@/assets/css/reset.css";
new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
