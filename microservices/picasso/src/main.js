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
Vue.prototype.$privateKey = '<?xml version="1.0" encoding="utf-16"?> <RSAParameters xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"> <D>lCCUcz2Unpz3AAgnlRYnxxAcyaKGDRWS9lOAtuV4F8E1r9pjpxeazXEImQQR0ILN7O9RXJJTIuyF3yBAzKRb6ZHW6/GqRzUgHi+Djg4dRjG5IR0F0mh8862rgG66G5bNlZO/OTIuBoaOLO8d+TjJodgNYEYgGd0ek3GuMz1jxv6U+u4YVchKCSp77KZEid2aPbcXZ0CuZhcWg6Jjh+ocUIDfi9GkGd+kIVW+jOOxbD9kdls1AMx5AREaueVdTaO9IBPJtqCxJiBZEWB/Kzh/VRWkicf8hyE3Qy2Wx9sZ9VkMPrlUXseOoeo14+M26/KmuvXpext3GbX3a4MSEyqLwQ==</D> <DP>JUoBvgL/Svq0A7Y6x8CJzCBrCPPsL2IMsP/dBACxIRJlImpOBxgGT+vi5Wuj2C7nvnAa1fPvIkDXygj3frsWpyHwRLOoZE2fud4xTV0l+PlGMy7Oa9LhrhVOHYLGyNZOFHv3tPb3OpMebV8zpMweALApzVnqALjet+y/dxgUni0=</DP> <DQ>IhzNZGC+/VBEezOL0Ubqk799JeOhekJ8RFyrMHiNkoJ5P63K6WDBIYmrWduYGlbJYpA2Z0uuBmtc86uc2tvhgNtC5sypzYeiqVp29cM9yBLvuk4VCgDDFdB3nc5qn42+o+QWC0dZ4Oadf6YszsbJeeSJtm2A8ZmnGZqhfUaZISE=</DQ> <Exponent>AQAB</Exponent> <InverseQ>vzrDTi/g6OPz/16XveQ0oxzAjeEYfWmxn/thQzhSsOitbwIxeanRtms7V7BgNYUI1elzXqIfkANofRtuv8bk2XGVpzMSawyUJrsO+XcwqIJk9XgwzY9esgwcJfIX4QtsEu8yqSIGVSTFUFSO0XZfkQoWkuzcxNLIm0wRPNUiAD0=</InverseQ> <Modulus>rfDgAWdbLx6R6gZbDkvTWPG6vIvc02ZWuDsTMrMwBEygWEoHy50059cKARhBOn/r9pM0Vzkln3sOgbyZgBz0gmxMl1oz3RTq9OlohRn2oovaDT9ovU2ctmfoeDlUZV/31kiYmgQ5vkWyXBCw+50jPn7E+PmjwdgDak2EYxNK6R1mL5ZPpvV1E277Ov+p0ayxc8DjdX+d3VULpaFW+WAAEjZ3XhtkxTa+DTxiYWIJ/zEkdJmsKXuDL509lickqOvUC15PqtjlrNlHAt23k82RyWwnoR6n07omJc59Ax+ksmDPf9qTmWrJy8NIrWY0+2Yus3i6XHWIfcQctFyQNC0Ykw==</Modulus> <P>4yme0Qs3HI4usrcx+L/uBp7YAeD/LKW4BHylJItIVcAWk//R9riAf5ml5BNypWbYe4vnliACDQovkNeJAvbxWMYpLsga11s8SZ+nLo5hhRkoIZ/OYENU4V+2lY6L6ahPEeVaTh2DDwwo8POynVXH3CqzvsqiKaoI3wF1+l+PEbM=</P> <Q>xAWlNXKeeOEKvWcHZz86WlNLvgZ+xjMwewbWB0/MLEPfRtdobIr0OOWZxNpP0UlqZz5x4aeL3Xa/zemcQl4l3GrWxHwvuaQE9dtraR7aehQ9+snHCICvxHZedcyEkTDetDcU7ZpYITtKJWNhR/nmeKvuBQ4mRfS/Z8Nf6TJPraE=</Q> </RSAParameters>';
Vue.prototype.$apiHeaders = {
  "Authorization": "BEARER " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5NTYyNTA2MywiZXhwIjoxNTk1NzExNDYzLCJpYXQiOjE1OTU2MjUwNjN9.vd14yXJKBuifN0cHC0hNuo5mZnacz-m0qhVuGUit_hs",
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
