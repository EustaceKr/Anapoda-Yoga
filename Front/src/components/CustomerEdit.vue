<script setup>
import Modal from '../components/Modal.vue'
import { ref } from 'vue'
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

const formValues = {
    id: null,
    firstName: '',
    lastName: '',
    username:'',
    password: '',
}

const schema = Yup.object().shape({
    firstName: Yup.string().required('First name is required'),
    lastName: Yup.string().required('Last Name is required')
});

const modalEdit = ref(null);

const onSubmitEdit = async(values, {setErrors}) => {
    const { id, firstName, lastName } = values;
    modalEdit.value.hide();
    return customersStore.editCustomer(id, firstName, lastName)
            .catch(error => setErrors({ apiError: error }));
}

function showEditModal(firstName, lastName){
    formValues.firstName = firstName;
    formValues.lastName = lastName;
    modalEdit.value.show();
}
</script>
<template>
    <Modal ref="modalEdit" maxWidth="800px" width="300px">
        <template #header>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Edit Customer
            </label>
        </template>
        <Form @submit="onSubmitEdit" :validation-schema="schema" :initial-values="formValues" v-slot="{ errors,isSubmitting }">
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
                <button class="btn btn-primary" :disabled="isSubmitting">
                    <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
                    Submit
                </button>
            </div>
        </Form>
    </Modal>
</template>