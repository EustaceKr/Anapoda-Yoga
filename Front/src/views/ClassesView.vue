<script setup>
import { storeToRefs } from 'pinia';
import { useClassesStore } from '@/stores';
//import Modal from '../components/Modal.vue'
//import { ref } from 'vue'
//import { Form, Field } from 'vee-validate';
//import * as Yup from 'yup';

const classesStore = useClassesStore();
const { yogaClasses  } = storeToRefs(classesStore);
classesStore.getAll();
console.log()
</script>
<template>
    <div>
        <ul v-if="yogaClasses.length">
            <li v-for="yogaClass in yogaClasses" :key="yogaClass.yogaClassId">{{yogaClass.yogaClassId}}</li>
        </ul>
    </div>
</template>