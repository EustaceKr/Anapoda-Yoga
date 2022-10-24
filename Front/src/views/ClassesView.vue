<script setup>
import { storeToRefs } from 'pinia';
import { useClassesStore,useClassTypesStore } from '@/stores';
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';
import Modal from '../components/Modal.vue'
import { ref } from 'vue'

const classesStore = useClassesStore();
const { yogaClasses  } = storeToRefs(classesStore);
classesStore.getAll();

const classTypesStore = useClassTypesStore();
const { classTypes } = storeToRefs(classTypesStore);
classTypesStore.getAll();

function getYogaClassTypeName(id){
    return classTypes.value.find(x => x.yogaClassTypeId == id).name;
}

function showModal(){
    modal.value.show();
}

const modal = ref(null)
const schema = Yup.object().shape({
    title: Yup.string().required('Title is required'),
});

const onSubmit = async(values, { setErrors }) => {
    const { id,title } = values;
    modal.value.hide();
    if(id){
       console.log(5);
    }else{
        return classesStore.saveClass(title, new Date())
            .catch(error => setErrors({ apiError: error }));
    }
}

</script>
<template>
    <div>
        <ul v-if="yogaClasses.length">
            <li v-for="yogaClass in yogaClasses" :key="yogaClass.yogaClassId">{{getYogaClassTypeName(yogaClass.yogaClassTypeId)}}</li>
        </ul>
    </div>
    <div>
        <button @click="showModal" class="btn btn-primary">Create a class</button>
    </div>
    <Modal ref="modal" maxWidth="800px" width="300px">
        <template #header>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Yoga Class Create
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors,isSubmitting }">
            <div class="form-group">
                <label>Title</label>
                <Field name="title" type="text" class="form-control" :class="{ 'is-invalid': errors.title }" />
                <div class="invalid-feedback">{{errors.title}}</div>
            </div>
            <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{errors.apiError}}</div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="isSubmitting">
                    <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
                    Submit
                </button>
            </div>
        </Form>
    </Modal>
</template>