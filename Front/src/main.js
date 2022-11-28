import { createApp } from 'vue';
import App from './App.vue';
import { createPinia } from 'pinia';
import { router } from './helpers';
import Vue3Toasity from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(Vue3Toasity,{
    autoClose: 2000,
})

app.mount('#app');
