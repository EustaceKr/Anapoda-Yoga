import { createRouter, createWebHistory } from "vue-router";
import HomeComp from "../views/HomeComp.vue";
import AboutComp from "../views/AboutComp.vue";
import CustomersComp from "../views/CustomersComp.vue";
import LoginComp from "../views/LoginComp.vue"
import TestComp from "../views/TestComp.vue"
import ReservationComp from "../views/ReservationComp.vue"
import YogaClassTypeComp from "../views/YogaClassTypeComp.vue"


const routes = [
  {
    path: "/",
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
    path: "/",
    name: "TestComp",
    component: TestComp
  },
  {
    path: "/",
    name: "ReservationComp",
    component: ReservationComp
  },
  {
    path: "/",
    name: "YogaClassTypeComp",
    component: YogaClassTypeComp
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
