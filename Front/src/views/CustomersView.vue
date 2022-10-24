<script setup>
import { storeToRefs } from 'pinia';
import { useCustomersStore } from '@/stores';
import Modal from '../components/Modal.vue'
import { ref } from 'vue'
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

const customersStore = useCustomersStore();
const { customers } = storeToRefs(customersStore);
customersStore.getAll();

function getDate(datetime){
    var date = new Date(datetime)
    if(date != 'Mon Jan 01 0001 00:00:00 GMT+0134 (Eastern European Standard Time)')
        return date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear()
    else return null
}

const modalCreate = ref(null);
const formValues = {
    id: null,
    firstName: '',
    lastName: '',
    username:'',
    password: '',
}

const schema = Yup.object().shape({
    firstName: Yup.string().required('First name is required'),
    lastName: Yup.string().required('Last Name is required'),
    username: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required')
});

function showCreateModal(){
    modalCreate.value.show();
}

const onSubmit = async(values, { setErrors }) => {
    const { id, firstName, lastName, username, password } = values;
    modalCreate.value.hide();
    if(id){
        return customersStore.editCustomer(id, firstName, lastName)
            .catch(error => setErrors({ apiError: error }));
    }else{
        return customersStore.saveCustomer(firstName, lastName, username, password)
            .catch(error => setErrors({ apiError: error }));
    }
}

</script>
<template>
    <button @click="showCreateModal()">Add new customer</button>
    <table class="table" v-if="customers.length">
        <thead>
            <tr>
                <th scope="col">First name</th>
                <th scope="col">Last Name</th>
                <th scope="col">Created Date</th>
                <th scope="col">Updated Date</th>
                <th scope="col">Delete</th>
                <th scope="col">Edit</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="customer in customers" :key="customer.id">
                <td>{{customer.firstName}}</td>
                <td>{{customer.lastName}}</td>
                <td>{{getDate(customer.createdDate)}}</td>
                <td>{{getDate(customer.updatedDate)}}</td>
                <!-- <td><button @click.prevent="deleteType(classType.yogaClassTypeId)">Delete</button></td> -->
                <!-- <td><button @click.prevent="showModal(classType.yogaClassTypeId,classType.name,classType.description,classType.duration,classType.capacity)">Edit</button></td> -->
            </tr>
        </tbody>
    </table>
    <Modal ref="modalCreate" maxWidth="800px" width="300px">
        <template #header>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Create Customer
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" :initial-values="formValues" v-slot="{ errors,isSubmitting }">
            <div class="form-group">
                <label>First Name</label>
                <Field name="firstName" type="text" class="form-control" :class="{ 'is-invalid': errors.firstName }" />
                <div class="invalid-feedback">{{errors.firstName}}</div>
            </div>
            <div class="form-group">
                <label>Last Name</label>
                <Field name="lastName" type="text" class="form-control"
                    :class="{ 'is-invalid': errors.lastName }" />
                <div class="invalid-feedback">{{errors.lastName}}</div>
            </div>
            <div class="form-group">
                <label>Username</label>
                <Field name="username" type="text" class="form-control" :class="{ 'is-invalid': errors.username }" />
                <div class="invalid-feedback">{{errors.username}}</div>
            </div>
            <div class="form-group">
                <label>Password</label>
                <Field name="password" type="text" class="form-control" :class="{ 'is-invalid': errors.password }" />
                <div class="invalid-feedback">{{errors.password}}</div>
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