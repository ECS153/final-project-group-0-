import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Settings from "../views/Account.vue";
import Credentials from "../views/Credentials.vue";
import Logs from "../views/Logs.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/account",
    name: "Account",
    component: Settings
  },
  {
    path: "/credentials",
    name: "Credentials",
    component: Credentials
  },
  {
    path: "/logs",
    name: "Logs",
    component: Logs
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
