<script setup>
  import { RouterLink, RouterView } from 'vue-router';
  import { useAuthStore } from '@/stores';
  
  const authStore = useAuthStore();
  </script>
  
  <template>
      <div class="app-container bg-light">
          <nav class="navbar navbar-expand navbar-dark bg-dark">
              <div class="navbar-nav">
                  <RouterLink to="/" class="nav-item nav-link">Home</RouterLink>
                  <RouterLink v-if="authStore.checkIfAdmin()" to="/classtypes" class="nav-item nav-link" >Class Types</RouterLink>
                  <RouterLink v-if="authStore.checkIfAdmin()" to="/yogaclasses" class="nav-item nav-link" >Classes</RouterLink>
                  <RouterLink v-if="authStore.checkIfAdmin()" to="/customers" class="nav-item nav-link" >Customers</RouterLink>
                  <RouterLink v-if="!authStore.user" to="/login" class="nav-item nav-link">Login</RouterLink>
                  <a v-if="authStore.user" @click="authStore.logout()" class="nav-item nav-link">Logout</a>
              </div>
          </nav>
          <div class="container-fluid pt-4 pb-5">
              <RouterView />
          </div>
      </div>
  </template>
  
  <style>
  @import '@/assets/base.css';
  </style>