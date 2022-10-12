<script setup>
import { storeToRefs } from 'pinia';
import { useClassesStore,useClassTypesStore } from '@/stores';
//import Modal from '../components/Modal.vue'
//import { ref } from 'vue'
//import { Form, Field } from 'vee-validate';
//import * as Yup from 'yup';

const classesStore = useClassesStore();
const { yogaClasses  } = storeToRefs(classesStore);
classesStore.getAll();

const classTypesStore = useClassTypesStore();
const { classTypes } = storeToRefs(classTypesStore);
classTypesStore.getAll();

function getYogaClassTypeName(id){
    console.log(classTypes.value);
    return classTypes.value.find(x => x.yogaClassTypeId == id).name;
}
</script>
<template>
    <div>
        <ul v-if="yogaClasses.length">
            <li v-for="yogaClass in yogaClasses" :key="yogaClass.yogaClassId">{{getYogaClassTypeName(yogaClass.yogaClassTypeId)}}</li>
        </ul>
    </div>
</template>