<script setup>
import { YogaClassesService, YogaClassTypesService, ReservationsService, CustomersService } from '@/services';
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';
import Modal from '../components/Modal.vue'
import { ref } from 'vue'
import { toast } from 'vue3-toastify';

const yogaClasses = ref([]);
const YogaClassTypes = ref([])
const customers = ref([]); ``
const reservationEdit = ref(false);
const reservationAdd = ref(false);
const year = new Date().getFullYear()
const month = ((new Date().getMonth() + 1).toString()).length == 1 ? '0' + (new Date().getMonth() + 1) : (new Date().getMonth() + 1).toString()
const day = new Date().getDate().toString().length == 1 ? '0' + new Date().getDate() : new Date().getDate()
const selectedDate = ref(year + '-' + month + '-' + day);


function callForYogaTypes() {
    YogaClassTypesService.getAll().then(x => YogaClassTypes.value = x);
}

function callForCustomers() {
    CustomersService.getAll().then(x => customers.value = x)
}

function callForClasses() {
    YogaClassesService.getAllByDate(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate()).then(x => yogaClasses.value = x);
}
callForCustomers();
callForYogaTypes();
callForClasses();

const formValues = {
    classId: null,
    title: null,
    customer: null,
    date: null,
    customer: null
}
const modal = ref(null);
const schema = Yup.object().shape({
    title: Yup.string().required('Title is required'),
    date: Yup.date().min(new Date()),
    customer: Yup.string().required('Customer is required')
});

//Helpers -Start
function getYogaClassTypeName(id) {
    if (Array.isArray(YogaClassTypes.value)) return YogaClassTypes.value.find(x => x.yogaClassTypeId == id).name;
    else return id
}

function getYogaClassTypeCapacity(id){
    if (Array.isArray(YogaClassTypes.value)) return YogaClassTypes.value.find(x => x.yogaClassTypeId == id).capacity;
    else return 0
}

function getYogaClassDate(datetime) {
    var date = new Date(datetime)
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear()
}

function getYogaClassTime(datetime) {
    var date = new Date(datetime)
    const enGB = date.toLocaleTimeString('en-GB', {
        hour: '2-digit',
        minute: '2-digit',
    });
    return enGB;
}

const GetClassesByDate = (date) => {
    YogaClassesService.getAllByDate(date).then(x => yogaClasses.value = x);
}
//Helpers - End

//Modals - Start
function showModal(classId, title, date) {
    if (date == null) {
        reservationEdit.value = false;
        formValues.title = null;
        formValues.date = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate() + 'T' + (new Date().getHours() + 1) + ':' + new Date().getMinutes() + ':00';
        formValues.time = null;
        formValues.customer = "NoNeedForCustomer";
        modal.value.show();
    } else {
        reservationEdit.value = false;
        formValues.classId = classId;
        formValues.title = title;
        formValues.date = date;
        formValues.customer = "NoNeedForCustomer";
        modal.value.show();
    }
}

function showReservationsModal(classId, title, date, isAdd) {
    if (isAdd) {
        reservationAdd.value = true
        reservationEdit.value = true
        CustomersService.getNotInYogaClass(classId).then(x => customers.value = x);
        formValues.classId = classId;
        formValues.title = title;
        formValues.date = date;
        formValues.customer = null;
        modal.value.show();
    } else {
        reservationAdd.value = false
        reservationEdit.value = true
        CustomersService.getInYogaClass(classId).then(x => customers.value = x);
        formValues.classId = classId;
        formValues.title = title;
        formValues.date = date;
        formValues.customer = null;
        modal.value.show();
    }

}
//Modals - End

//Form - Start
const onSubmit = async (values, { setErrors }) => {
    const { classId, title, date, customer } = values;
    modal.value.hide();
    if (!reservationEdit.value) {
        if (classId) {
            var response = await YogaClassesService.editClass(classId, title, date, selectedDate.value)
                .catch(error => setErrors({ apiError: error }));
            YogaClassesService.getAllByDate(selectedDate.value).then(x => yogaClasses.value = x);
            if (response == 200) toast.success("Class successfully editted.", { posistion: toast.POSITION.TOP_RIGHT })
            else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
        } else {
            var response = await YogaClassesService.saveClass(title, date, selectedDate.value)
                .catch(error => setErrors({ apiError: error }));
            YogaClassesService.getAllByDate(selectedDate.value).then(x => yogaClasses.value = x);
            if (response == 201) toast.success("Class successfully added.", { posistion: toast.POSITION.TOP_RIGHT })
            else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
        }
    } else if (reservationEdit) {
        if (reservationAdd.value) {
            var response = await ReservationsService.adminSaveReservation(classId, customer)
                .catch(error => setErrors({ apiError: error }));
            callForClasses();
            if (response == 200) {
                toast.success("Reservation successfully added.", { posistion: toast.POSITION.TOP_RIGHT })
            }
            else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
        } else if (!reservationAdd.value) {
            var response = await ReservationsService.adminDeleteReservation(classId, customer)
                .catch(error => setErrors({ apiError: error }));
            callForClasses();
            if (response == 200) {
                YogaClassesService.getAllByDate(selectedDate.value).then(x => yogaClasses.value = x);
                toast.warning("Reservation successfully cancelled.", { posistion: toast.POSITION.TOP_RIGHT })
            }
            else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
        }
    }
}

const deleteYogaClass = async (id) => {
    var response = await YogaClassesService.deleteClass(id, selectedDate.value)
    callForClasses();
    if (response == 200) toast.warning("Class successfully deleted.", { posistion: toast.POSITION.TOP_RIGHT })
    else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
}

//Form - End

</script>
<template>
    <div>
        <button @click="showModal(null, null, null)" class="btn btn-primary">Create a class</button>
    </div>
    <div style=" margin: auto;width: 10%;padding: 10px;">
        <input style="text-align:center; margin:auto;" type="date" v-model="selectedDate"
            @change="GetClassesByDate(selectedDate)" />
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
                <td>{{ getYogaClassTypeName(yogaClass.yogaClassTypeId) }}</td>
                <td>{{ getYogaClassDate(yogaClass.date) }}</td>
                <td>{{ getYogaClassTime(yogaClass.date) }}</td>
                <td>{{ yogaClass.reservations.length }} / {{ getYogaClassTypeCapacity(yogaClass.yogaClassTypeId) }}</td>
                <td><button @click.prevent="deleteYogaClass(yogaClass.yogaClassId)">Delete</button></td>
                <td><button
                        @click.prevent="showModal(yogaClass.yogaClassId, yogaClass.yogaClassTypeId, yogaClass.date)">Edit</button>
                </td>
                <td>
                    <button
                        @click.prevent="showReservationsModal(yogaClass.yogaClassId, getYogaClassTypeName(yogaClass.yogaClassTypeId), yogaClass.date, true)">Add
                        Reservation
                    </button>
                </td>
                <td>
                    <button
                        @click.prevent="showReservationsModal(yogaClass.yogaClassId, getYogaClassTypeName(yogaClass.yogaClassTypeId), yogaClass.date, false)">Cancel
                        Reservation
                    </button>
                </td>
            </tr>
        </tbody>
    </table>
    <Modal ref="modal" maxWidth="800px" width="300px">
        <template #header v-if="!reservationEdit">
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Create Yoga Class
            </label>
        </template>
        <template #header v-else-if="reservationAdd">
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Add a reservation
            </label>
        </template>
        <template #header v-else>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Cancel a reservation
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" :initial-values="formValues"
            v-slot="{ errors, isSubmitting }">
            <div class="form-group" v-show="!reservationEdit">
                <label>Title</label>
                <Field name="title" as="select" class="form-control" :class="{ 'is-invalid': errors.title }">
                    <option value="">Select a class type</option>
                    <option v-for="classType in YogaClassTypes" :value="classType.yogaClassTypeId"
                        :key="classType.yogaClassTypeId">{{ classType.name }}</option>
                </Field>
                <div class="invalid-feedback">{{ errors.title }}</div>
            </div>
            <div class="form-group" v-show="!reservationEdit">
                <label>Date</label>
                <Field name="date" type="datetime-local" class="form-control" :class="{ 'is-invalid': errors.date }" />
                <div class="invalid-feedback">{{ errors.date }}</div>
            </div>
            <div v-show="reservationEdit">
                <div class="form-group">
                    {{ formValues.title }} - {{ formValues.date }} - {{ formValues.time }}
                </div>
                <div class="form-group">
                    <label v-if="reservationAdd">Add a customer</label>
                    <label v-else>Remove a customer</label>
                    <Field name="customer" as="select" class="form-control" :class="{ 'is-invalid': errors.title }">
                        <option value="">Select a customer</option>
                        <option v-for="customer in customers" :value="customer.id" :key="customer.id">
                            {{ customer.firstName }} {{ customer.lastName }}</option>
                    </Field>
                    <div class="invalid-feedback">{{ errors.customer }}</div>
                </div>
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