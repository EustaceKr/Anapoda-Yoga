import { createRouter, createWebHistory } from "vue-router";
import HomeComp from "../views/HomeComp.vue";
import AboutComp from "../views/AboutComp.vue";
import CustomersComp from "../views/CustomersComp.vue";
import LoginComp from "../views/LoginComp.vue"
import TestComp from "../views/TestComp.vue"


const routes = [
  {
    path: "/Home",
    name: "HomeComp",
    component: HomeComp,
  },
  {
    path: "/",
    name: "AboutComp",
    component: AboutComp,
  },
  {
    path: "/",
    name: "CustomersComp",
    component: CustomersComp,
  },
  {
    path: "/",
    name: "LoginComp",
    component: LoginComp
  },
  {
    path: "/Test",
    name: "TestComp",
    component: TestComp
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
