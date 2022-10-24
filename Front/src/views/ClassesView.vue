<script setup>
import { storeToRefs } from 'pinia';
import { useClassesStore,useClassTypesStore,useReservationsStore } from '@/stores';
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

const reservationsStore = useReservationsStore();
const { reservations } = storeToRefs(reservationsStore);

function getYogaClassTypeName(id){
    return classTypes.value.find(x => x.yogaClassTypeId == id).name;
}

function getYogaClassDate(datetime){
    var date = new Date(datetime)
    return date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear()
}

function getYogaClassTime(datetime){
    var date = new Date(datetime)
    return date.getHours() + ':' + date.getMinutes()
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

function reserve(yogaClassId){
    return reservationsStore.saveReservation(yogaClassId)
        .then(classesStore.getAll())
        .catch(error => setErrors({ apiError: error }));
}

</script>
<template>
     <div>
        <button @click="showModal" class="btn btn-primary">Create a class</button>
    </div>
    <table class="table" v-if="yogaClasses.length">
        <thead>
            <tr>
                <th scope="col">Class Type Name</th>
                <th scope="col">Date</th>
                <th scope="col">Time</th>
                <th scope="col">Reservations</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="yogaClass in yogaClasses" :key="yogaClass.yogaClassId">
                <td>{{getYogaClassTypeName(yogaClass.yogaClassTypeId)}}</td>
                <td>{{getYogaClassDate(yogaClass.date)}}</td>
                <td>{{getYogaClassTime(yogaClass.date)}}</td>
                <td>{{yogaClass.reservations.length}}</td>
                <td><button @click.prevent="reserve(yogaClass.yogaClassId)">Edit Reservations</button></td>
            </tr>
        </tbody>
    </table>
    <Modal ref="modal" maxWidth="800px" width="300px">
        <template #header>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Yoga Class Create
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors,isSubmitting }">
            <div class="form-group">
                <label>Title</label>
                <Field name="title" as="select" class="form-control" :class="{ 'is-invalid': errors.title }">
                    <option value="">Select a class type</option>
                    <option v-for="classType in classTypes" :value="classType.yogaClassTypeId" :key="classType.yogaClassTypeId">{{classType.name}}</option>
                </Field>
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