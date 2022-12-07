<script setup>
import { CustomersService } from '@/services';
import Modal from '../components/Modal.vue'
import { ref } from 'vue'
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const customers = ref([])

callForCustomers();
function callForCustomers() {
    CustomersService.getAll().then(x => customers.value = x);
}

const modal = ref(null);
const formValues = {
    id: null,
    firstName: '',
    lastName: '',
    phone: '',
    mobileNumber: '',
    dateOfBirth: '',
    sex: '',
    adress: '',
    city: '',
    state: '',
    postalCode: '',
    payDate: '',
    timesPerMonth: '',
    username: '',
    password: '',
}
const schema = Yup.object().shape({
    username: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required'),
    firstName: Yup.string().required('First name is required'),
    lastName: Yup.string().required('Last Name is required'),
});
const isCreate = ref(true);

function getDateForm(datetime) {
    var date = new Date(datetime)
    if (date != 'Mon Jan 01 0001 00:00:00 GMT+0134 (Eastern European Standard Time)') {
        var year = date.getFullYear()
        var month = date.getMonth().toString().length == 1 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1
        var day = date.getDate().toString().length == 1 ? '0' + date.getDate() : date.getDate()
        return year + '-' + month + '-' + day
    }
    else return null
}

function showModal(id, firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth) {
    if (id) {
        formValues.id = id;
        formValues.username = 'NoNeedForUserName'
        formValues.password = 'NoNeedForPassword'
        formValues.firstName = firstName;
        formValues.lastName = lastName;
        formValues.phone = phone;
        formValues.mobileNumber = mobileNumber;
        formValues.dateOfBirth = getDateForm(dateOfBirth);
        formValues.sex = sex;
        formValues.adress = adress;
        formValues.city = city;
        formValues.state = state;
        formValues.postalCode = postalCode;
        formValues.payDate = getDateForm(payDate);
        formValues.timesPerMonth = timesPerMonth;
        isCreate.value = false;
        modal.value.show();
    } else {
        formValues.id = null;
        formValues.username = '';
        formValues.password = '';
        formValues.firstName = '';
        formValues.lastName = '';
        formValues.phone = '';
        formValues.mobileNumber = '';
        formValues.dateOfBirth = '';
        formValues.sex = '';
        formValues.adress = '';
        formValues.city = '';
        formValues.state = '';
        formValues.postalCode = '';
        formValues.payDate = '';
        formValues.timesPerMonth = '';
        isCreate.value = true;
        modal.value.show();
    }
}

const onSubmit = async (values, { setErrors }) => {
    const { id, firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth, username, password } = values;
    modal.value.hide();
    if (id) {
        formValues.username = '';
        formValues.password = '';
        var response = await CustomersService.editCustomer(id, firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth)
            .catch(error => setErrors({ apiError: error }));
        callForCustomers()
        if (response == 200) toast.success("Customer successfully editted.", { posistion: toast.POSITION.TOP_RIGHT })
        else toast.error(`Something went wrong`, { posistion: toast.POSITION.TOP_RIGHT })
    } else {
        var response = await CustomersService.saveCustomer(firstName, lastName, phone, mobileNumber, dateOfBirth, sex, adress, city, state, postalCode, payDate, timesPerMonth, username, password)
            .catch(error => setErrors({ apiError: error }));
        callForCustomers()
        if (response == 201) toast.success("Customer successfully added.", { posistion: toast.POSITION.TOP_RIGHT })
        else toast.error(`Something went wrong ${response}`, { posistion: toast.POSITION.TOP_RIGHT })
    }
}

const deleteCustomer = async (id) => {
    var response = await CustomersService.deleteCustomer(id)
        .catch(error => setErrors({ apiError: error }));
    callForCustomers();
    if (response == 200) toast.warning("Customer successfully deleted.", { posistion: toast.POSITION.TOP_RIGHT })
    else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
}
</script>
<template>
    <button @click="showModal(null, null, null)">Add Customer</button>
    <table class="table" v-if="customers.length">
        <thead class="thead-dark" style="width:100%">
            <tr>
                <th scope="col">First name</th>
                <th scope="col">Last Name</th>
                <th scope="col">Phone</th>
                <th scope="col">Mobile Number</th>
                <th scope="col">Date Of Birth</th>
                <th scope="col">Sex</th>
                <th scope="col">Adress</th>
                <th scope="col">City</th>
                <th scope="col">State</th>
                <th scope="col">Postal Code</th>
                <th scope="col">Pay Date</th>
                <th scope="col">Times Per Month</th>
                <th scope="col">Edit</th>
                <th scope="col">Delete</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="customer in customers" :key="customer.id">
                <td>{{ customer.firstName }}</td>
                <td>{{ customer.lastName }}</td>
                <td>{{ customer.phone }}</td>
                <td>{{ customer.mobileNumber }}</td>
                <td>{{ new Date(customer.dateOfBirth).toLocaleDateString() }}</td>
                <td>{{ customer.sex }}</td>
                <td>{{ customer.adress }}</td>
                <td>{{ customer.city }}</td>
                <td>{{ customer.state }}</td>
                <td>{{ customer.postalCode }}</td>
                <td>{{ new Date(customer.payDate).toLocaleDateString() }}</td>
                <td>{{ customer.timesPerMonth }}</td>
                <td><button
                        @click.prevent="showModal(customer.id, customer.firstName, customer.lastName, customer.phone, customer.mobileNumber, customer.dateOfBirth,
                        customer.sex, customer.adress, customer.city, customer.state, customer.postalCode, customer.payDate, customer.timesPerMonth)">Edit</button>
                </td>
                <td><button @click.prevent="deleteCustomer(customer.id)">Delete</button></td>
            </tr>
        </tbody>
    </table>
    <Modal ref="modal" maxWidth="1400px" width="700px">
        <template #header v-if="isCreate">
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Create Customer
            </label>
        </template>
        <template #header v-else>
            <label class="col-form-label mb-2" style="font-size: 1.15em;">
                Edit Customer
            </label>
        </template>
        <Form @submit="onSubmit" :validation-schema="schema" :initial-values="formValues"
            v-slot="{ errors, isSubmitting }">
            <div class="form-group">
                <label>First Name</label>
                <Field name="firstName" type="text" class="form-control" :class="{ 'is-invalid': errors.firstName }" />
                <div class="invalid-feedback">{{ errors.firstName }}</div>
            </div>
            <div class="form-group">
                <label>Last Name</label>
                <Field name="lastName" type="text" class="form-control" :class="{ 'is-invalid': errors.lastName }" />
                <div class="invalid-feedback">{{ errors.lastName }}</div>
            </div>
            <div class="form-group">
                <label>Phone</label>
                <Field name="phone" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>Mobile Number</label>
                <Field name="mobileNumber" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>Date of Birth</label>
                <Field name="dateOfBirth" type="date" class="form-control" format="yyyy-MM-dd" />
            </div>
            <div class="form-group">
                <label>Sex</label>
                <Field name="sex" as="select" class="form-control">
                    <option value="''">Select sex</option>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                </Field>
            </div>
            <div class="form-group">
                <label>Adress</label>
                <Field name="adress" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>City</label>
                <Field name="city" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>State</label>
                <Field name="state" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>Postal Code</label>
                <Field name="postalCode" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>Pay Date</label>
                <Field name="payDate" type="date" class="form-control" />
            </div>
            <div class="form-group">
                <label>Times per Month</label>
                <Field name="timesPerMonth" type="number" class="form-control" />
            </div>
            <div class="form-group" v-show="isCreate">
                <label>Username</label>
                <Field name="username" type="text" class="form-control" :class="{ 'is-invalid': errors.username }" />
                <div class="invalid-feedback">{{ errors.username }}</div>
            </div>
            <div class="form-group" v-show="isCreate">
                <label>Password</label>
                <Field name="password" type="text" class="form-control" :class="{ 'is-invalid': errors.password }" />
                <div class="invalid-feedback">{{ errors.password }}</div>
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