<script setup>
import { YogaClassesService, YogaClassTypesService, ReservationsService } from '@/services';
import { toast } from 'vue3-toastify';
import { ref } from 'vue'

const year = new Date().getFullYear();
const month = ((new Date().getMonth() + 1).toString()).length == 1 ? '0' + (new Date().getMonth() + 1) : (new Date().getMonth() + 1).toString();
const day = new Date().getDate().toString().length == 1 ? '0' + new Date().getDate() : new Date().getDate();
const selectedDate = ref(year + '-' + month + '-' + day);
const yogaClasses = ref([]);
const YogaClassTypes = ref([]);
const UserReservations = ref([]);

const GetClassesByDate = (date) => {
    YogaClassesService.getAllByDate(date).then(x => yogaClasses.value = x);
}

function getYogaClassTypeName(id) {
    if (Array.isArray(YogaClassTypes.value)) return YogaClassTypes.value.find(x => x.yogaClassTypeId == id).name;
    else return id
}

function getYogaClassTypeCapacity(id){
    if (Array.isArray(YogaClassTypes.value)) return YogaClassTypes.value.find(x => x.yogaClassTypeId == id).capacity;
    else return 0
}

function callForYogaTypes() {
    YogaClassTypesService.getAll().then(x => YogaClassTypes.value = x);
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

function callForClasses() {
    YogaClassesService.getAllByDate(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate()).then(x => yogaClasses.value = x);
}

const addCustomerReservation = async (id) => {
    var response =  await ReservationsService.saveReservation(id);
    GetClassesByDate(selectedDate.value);
    callForReservations();
    if (response == 200) {
        toast.success("Reservation successfully added.", { posistion: toast.POSITION.TOP_RIGHT })
    }
    else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
}

const deleteCustomerReservation = async (id) => {
    var response =  await ReservationsService.deleteReservation(id);
    GetClassesByDate(selectedDate.value);
    callForReservations();
    if (response == 200) {
        toast.warning("Reservation successfully deleted.", { posistion: toast.POSITION.TOP_RIGHT })
    }
    else toast.error("Something went wrong", { posistion: toast.POSITION.TOP_RIGHT })
}

function checkIfAbleToReserve(id, reservationsLength, capacity){
    for(var i=0; i<UserReservations.value.length; i++){
        if(UserReservations.value[i].yogaClassId == id) return false;
    }
    if(reservationsLength >= capacity) return false;
    return true
}

function checkIfAbleToCancel(id, date){
    var d1 = new Date();
    var d2 = new Date(date);
    d1.setTime(d1.getTime() + 2 * 60 * 60 * 1000);

    for(var i=0; i<UserReservations.value.length; i++){
        if(UserReservations.value[i].yogaClassId == id && d1 < d2) return true;
    }

    return false;
}

async function callForReservations() {
    UserReservations.value = await ReservationsService.getAllByUser();
}

callForReservations();
callForClasses();
callForYogaTypes();

</script>
<template>
    <div style=" margin: auto;width: 10%;padding: 10px;">
        <input style="text-align:center; margin:auto;" type="date" v-model="selectedDate"
            @change="GetClassesByDate(selectedDate)" />
    </div>
    <table class="table" v-if="yogaClasses.length">
        <thead>
            <tr>
                <th scope="col">Class Type</th>
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
                <td>
                    <button @click.prevent="addCustomerReservation(yogaClass.yogaClassId)" :disabled="!checkIfAbleToReserve(yogaClass.yogaClassId, yogaClass.reservations.length, getYogaClassTypeCapacity(yogaClass.yogaClassTypeId))">Reserve</button>
                </td>
                <td>
                    <button @click.prevent="deleteCustomerReservation(yogaClass.yogaClassId)" :disabled="!checkIfAbleToCancel(yogaClass.yogaClassId, yogaClass.date)">Cancel</button>
                </td>
            </tr>
        </tbody>
    </table>
</template>