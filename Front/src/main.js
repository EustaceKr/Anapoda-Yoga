import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from '../src/axios'
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";


createApp(App).use(store).use(router).use(axios).mount('#app')
