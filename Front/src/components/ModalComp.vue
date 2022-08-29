<template>
    <transition name="modal-animation">
    <div v-show="modalActive" class="modal">
      <transition name="modal-animation-inner">
        <div v-show="modalActive" class="modal-inner">
          <i @click="close" class="far fa-times-circle"></i>
          <!-- Modal Content -->
          <slot />
          <button @click="close" type="button">Close</button>
        </div>
      </transition>
    </div>
  </transition>
</template>

<script>
    export default {
      props: ["modalActive"],
      setup(props, { emit }) {
        const close = () => {
          emit("close");
        };
    
        return { close };
      },
    };
    </script>
  
<style lang="scss" scoped>
    
    </style>
  