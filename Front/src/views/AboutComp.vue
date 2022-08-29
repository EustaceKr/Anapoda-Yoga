<template>
<br/>
<br/>
<br/>
<br/>
<div class="home">
    <ModalComp @close="toggleModal" :modalActive="modalActive">
      <div class="modal-content">
        <h1>This is a Modal Header</h1>
        <p>This is a modal message</p>
      </div>
    </ModalComp>
    <button @click="toggleModal" type="button">Open Modal</button>
  </div>
</template>

<script>
import ModalComp from '@/components/ModalComp.vue';
import {ref} from 'vue'

export default {
  name: "AboutComp",
  components: { ModalComp },
  setup() {
    const modalActive = ref(false);

    const toggleModal = () => {
      modalActive.value = !modalActive.value;
    };

    return { modalActive, toggleModal };
  }
}
</script>

<style lang="scss" scoped>

</style>
