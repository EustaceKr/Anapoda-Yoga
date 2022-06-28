import { createRouter, createWebHistory } from "vue-router";
import HomeComp from "../views/HomeComp.vue";
import AboutComp from "../views/AboutComp.vue";
import CustomersComp from "../views/CustomersComp.vue";


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
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
