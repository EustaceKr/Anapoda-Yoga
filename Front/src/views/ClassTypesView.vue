<script setup>
import { YogaClassTypesService } from '@/services';
import Modal from '../components/Modal.vue'
import { ref } from 'vue'
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const classTypes = ref([])
callForTypes();

function callForTypes() {
    YogaClassTypesService.getAll().then(x => classTypes.value = x);
}


const modal = ref(null);
const formValues = {
    id: null,
    name: '',
    description: '',
    duration: '01:00:00',
    capacity: 7,
}
const schema = Yup.object().shape({
    name: Yup.string().required('Name is required'),
    description: Yup.string().required('Description is required'),
    capacity: Yup.string().required('Capacity is required'),
    duration: Yup.string().required('Duration is required')
});

function showModal(id, name, description, duration, capacity = null) {
    if (id) {
        formValues.id = id;
        formValues.name = name;
        formValues.description = description;
        formValues.duration = duration;
        formValues.capacity = capacity;
        modal.value.show();
    } else {
        formValues.name = '';
        formValues.description = '';
        formValues.duration = '01:00:00';
        formValues.capacity = 7;
        modal.value.show();
    }
}

const onSubmit = async (values, { setErrors }) => {
    const { id, name, description, capacity, duration } = values;
    modal.value.hide();
    if (id) {
        var response = await YogaClassTypesService.editClassType(id, name, description, capacity, duration)
            .catch(error => setErrors({ apiError: error }));
        callForTypes();
        if (response == 200) toast.success("Class Type successfully edited.", { posistion: toast.POSITION.TOP_RIGHT })
        else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
    } else {
        var response = await YogaClassTypesService.saveClassType(name, description, capacity, duration)
            .catch(error => setErrors({ apiError: error }));
        callForTypes();
        if (response == 201) toast.success("Class Type successfully added.", { posistion: toast.POSITION.TOP_RIGHT })
        else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
    }
}

const deleteType = async (id) => {
    var response = await YogaClassTypesService.deleteClassType(id)
        .catch(error => setErrors({ apiError: error }));
    callForTypes();
    if (response == 200) toast.warning("Class Type successfully deleted.", { posistion: toast.POSITION.TOP_RIGHT })
    else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
}

</script>
<template>
    <button @click="showModal(null, null, null, null, null)">Add new class type</button>
    <table class="table" v-if="classTypes.length">
        <thead>
            <tr>
                <th scope="col">Name</th>
                <th scope="col">Description</th>
                <th scope="col">Duration</th>
                <th scope="col">Capacity</th>
                <th scope="col">Delete</th>
                <th scope="col">Edit</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="classType in classTypes" :key="classType.yogaClassTypeId">
                <td>{{ classType.name }}</td>
                <td>{{ classType.description }}</td>
                <td>{{ classType.duration }}</td>
                <td>{{ classType.capacity }}</td>
                <td><button @click.prevent="deleteType(classType.yogaClassTypeId)">Delete</button></td>
                <td><button
                        @click.prevent="showModal(classType.yogaClassTypeId, classType.name, classType.description, classType.duration, classType.capacity)">Edit</button>
                </td>
            </tr>
        </tbody>
    </table>
    <Modal ref="modal" maxWidth="800px" width="300px">
        <template #header>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Yoga Class Type
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" :initial-values="formValues"
            v-slot="{ errors, isSubmitting }">
            <div class="form-group">
                <label>Name</label>
                <Field name="name" type="text" class="form-control" :class="{ 'is-invalid': errors.name }" />
                <div class="invalid-feedback">{{ errors.name }}</div>
            </div>
            <div class="form-group">
                <label>Description</label>
                <Field name="description" type="text" class="form-control"
                    :class="{ 'is-invalid': errors.description }" />
                <div class="invalid-feedback">{{ errors.description }}</div>
            </div>
            <div class="form-group">
                <label>Duration</label>
                <Field name="duration" type="text" class="form-control" :class="{ 'is-invalid': errors.duration }" />
                <div class="invalid-feedback">{{ errors.duration }}</div>
            </div>
            <div class="form-group">
                <label>Capacity</label>
                <Field name="capacity" type="text" class="form-control" :class="{ 'is-invalid': errors.capacity }" />
                <div class="invalid-feedback">{{ errors.capacity }}</div>
            </div>
            <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="isSubmitting">
                    <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
                    Submit
                </button>
            </div>
        </Form>
    </Modal>
</template>