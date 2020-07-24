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
Vue.prototype.$privateKey = '<?xml version="1.0" encoding="utf-16"?><RSAParameters xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><D>NzyCLE+QyePWF9vxOZglbHjj1aUG0XGTzIPzZrNotT5PHUZ4HqEUqjNYvKT5zm6cxVfOKTWRZGAeMTA95tQaWnX1qqtFREevSZfzl35/eyXssMR9PhzZui9cknQw+IoHR5OSLqxwYSQH0n100L5agh1VxK9XaUVUkTAeBsjwY9XaZuckhHauTahhWuQH+FuqJj2fHBAO3FRTpU3X2tYt8znWhomYWU2WwBRfJ8jRCZtKBFKmrMHgM9Xqky3yASZirj53BG/tCwUslWiBXp0Z+afOCfDIqTgBX1ly1CR5ISaRsjw4YBvKP9TQ9IfhqmjaaP0wQPw6vAcUfYNy552/QQ==</D><DP>fpwR4iPbDPQl5v6T7+vCNBU/TrtrS8lO1QkQzMaL5IqTe9l0HEUpPca+rxGSEWQ3u2tBdpiHFEWNofgDvjXiC8C0j98TnvMQY8C7io7p8W0SGe1GLhGV/lVLIo93BH7n/w/PMsM/HLCAQQqFiJLyECxsTVc5fTaAtVzl7oSVvIE=</DP><DQ>eRhxfszbtndDWmXhXnDFyOD1gZSUoMlh0tIWF5urwqvrZINAtYbPuc1VPNO6iG4znwFnoYFvSbvj8vmk5qZNEKS2j1ODeITtW+ewMG9WbRBFE0+j0sRUmXZuM3rj2GLBGZlBGH3P00phKDCQX2Vpnz0utGu+LtNmZakv9gGEKsE=</DQ><Exponent>AQAB</Exponent><InverseQ>WRF5IYanYaTSSIFjNQwPa70dPE1fSLehr3mbBT/y1ZfuPn8bFeivsQz7Q+ReI5Zvb1XzU2h/ohpojGhNaUoY57y5HRmWxUuui7tXSrXPIPpHKOXBprhGnMk3R36fVHqJLH255p2mrfAVMI9Zuaxsc+mPLPyLiu4yGNBYqp+l4Ek=</InverseQ><Modulus>xpjSc6Mng1nwjAW+zLq4Ns4szxTniphQa0uOq+LHjQwjqrBcM8/1X1VDIbr+FOdiFb+rhgkGYBWbr4g3mjwgVoR3x2LtZiTKQZq6nP0e3U1amHtWYY2e6Xz6IHswUp9tvKCUVvFv0KX2F45mVGw+QEO/iWzfe2AzjMo6LyymPVPsmVNc0vekKHSoPWkoRIpJFJsY9YM/giVPJQmmSIjw7y0zeTSJdVz7SMzdAaqvdv2qquoTH6QeVdqoEQY4Gcrz+YmS/Rzi2VF/S0hL/sMU9sRMREySyyXbYbBWv6j3nePgJy5pbxYw0d/GaXdS+uLlTODMsIpIw2LMVRhbKjX7kw==</Modulus><P>4kQGPgeTVBlfzG3e0RY94HFkU/TJ/a/EWC5IFSlGcaMEENggGIaEQ8wmWeoduyEf8Q3UAYWRg3BgVO0NVbxQ4IWWOxxk9OCcFR4m8qt63U//J8IfdwHQwNxTCuh/6nwQ9o+DXmXD6KdLZ0eHgUmuCgqyqj6YfeC2JGUpLsLSb9s=</P><Q>4LH43MqsPKxIuWJeUEP88FXOCTwO/7IWlhdfzVWqwFTCQAEykVze5T98VWrrCjXlifsPvBu44F0bEgQAAIuJai6ONNatdbXwWa4ZG6eukG3WZLHUwuLFel5Wd9xzaYVf0kR/sZj254vyrqkk0dQdBpn9sTegK7tExuHybQVZrKk=</Q></RSAParameters>';
Vue.prototype.$apiHeaders = {
  "Authorization": "BEARER " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5NTQ4NDgwMiwiZXhwIjoxNTk1NTcxMjAyLCJpYXQiOjE1OTU0ODQ4MDJ9.yWAYk6oeVemmwiOVyBexadmLp867ZBBsHnKUJANTOs0",
  "Content-Type": "application/json"
};

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
