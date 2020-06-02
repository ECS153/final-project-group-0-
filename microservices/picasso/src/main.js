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
Vue.prototype.$privateKey = '<?xml version="1.0" encoding="utf-16"?><RSAParameters xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><D>5C5wY1sOt8lgZSfqF1ACi5jgHFK++SmoqxA7yPcz02ILo2KxbQSyx8U7ojzpOLObCCWP722IDTq72TXF4uNpXlWNP6/Fsb93dp7VWaQtmZkjvvMV0bP4NTrQzNInh8qInvTdFj4EUSQm6ktJjh7vek02jS5b9B5ejtIvdtJJF3V+itlq9aPFMBy3qSuJzESh9+JGXQWtndnEQY2mEFMvdjAQfUf5xoDImep1Jf7wN/liiP7skXggJ9X3CAcyNuSHq2gxnzFVCYteMmpugSNlx/huHAq+69sHmO8lWiYO1A4If2t3nD+iHTzf8IYOgnxng8Wl3USfQIr+Wxi2NRREjQ==</D>  <DP>HkaMP13iq3rxKJ1AzOnrqKnk7m4/8zOgAR/YS4xQlvBgx7TzO6Mpj5KFLwnCO0CDbMCKzzM0+vkW3Z6pTQ5C71oBCkWQyKnspsddKjGZYcJqN7KWFijpSPTDV7uQVWGM1gioGYLVVP/Ywpy8vwM3xEp/HzvmKKV7w5CfjmNEEEc=</DP><DQ>Gn3O/1YARykhAk4noTvzf8iB6pZ8fwwDstdCgsHvY6JFc2pZB2HafqDKlKvFEPz32bUgm2J6qN1FO31r+QJ/06XjS8m629gJpAlkZJi1b2LLiuvH0oCuYHSuNNibarEchaXTxWIfVAti2jxSMWw/dftXGiU6TubYvvSN3dkcui8=</DQ><Exponent>AQAB</Exponent><InverseQ>o2XfEwX4USrpKOt3LQZh/mrXmGaBoGR4Tx7a9wGlsxjGW1xnLPJNKzMXQmUwAd4z7RwT6Nt9SaWvU+sgzhUbYXr68Sxs3Av0f0gqjyhdXm2uldP7UERL3bX//5chNPL99zL2XbR8RTeQ8Mdtm00UykgkIIncxj9SmNe/Ry8hm3c=</InverseQ><Modulus>67pFZDVKmvd9eVdDC2qeNZAtu9JhnYPnTnJ+uT5k4v07WI6+Eq0bzTPppFOcWVSRd1MHMvXnynQqpUVsyMHHlZ+LesWyzziXyW9U7zT4IlgQuRLYITGx/CV02c8d7h2QluFoc7b4V2yYnIyPOqf3e3cK+1nT86lAEwzKDA6CxwCNLASDtU/5VyC5wzaSMoBufmVyFbvBmunNB/nfQnSAj6PTfSn/E3TB4ceGUB7cuT/XV+8xTnUPKPrel9zDsb/uNSH68eEnnwcBNERgnVBG/8N3EazncSFF55sXJvO1FMk2fChQAdllsOI/gMpVhLicJRHMgyYdEf9pwrS/d5HAmQ==</Modulus><P>+bz+/5XdDSqFUYBiR/ERUmBDzfc/fa62NGMovZxwn1WagUOZS/V4Tn6Gq/bj1rdMPqM4WN4TmLM1/nApGwUO+HVMiPYDW8HrViDtt1U2VkheaaXm06nB32SzruU53jpkdAurIkgTEdtCfN/avm2UCsU9wo3iTzBgsscJR7gpGZc=</P><Q>8aNYJzp0aIRDiK6DKsbE4XcrwbvFny0rZFT9VBEJSLVSr4FQ+hTNrBofwzpjSqVH46xsea3Eng0dfjA5PJ+CO2uZlqTGcVWr2Fhgjmp2gg6R7fJ8tt63rSmswIBGzpmtyyNJ1RkStio3tiVIthV7Ca8otN5YlIvMeOcNhJ0BXU8=</Q></RSAParameters>';
Vue.prototype.$apiHeaders = {
  "Authorization": "BEARER " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwicm9sZSI6IlVzZXIiLCJuYmYiOjE1OTEwNzk5ODIsImV4cCI6MTU5MTE2NjM4MiwiaWF0IjoxNTkxMDc5OTgyfQ.Nk_84NUENoyTWO47YzrutL0OSz1HfzU2wp9eIYmP2JA",
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
