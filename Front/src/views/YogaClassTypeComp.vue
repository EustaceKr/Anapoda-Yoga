<template>
    <div>
        <br />
        <br />
        <br />
        <br />
        <button @click="toggleModal" type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">
            Create
        </button>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th hidden="true">Id</th>
                    <th>Name</th>
                    <th>Desciption</th>
                    <th>Capacity</th>
                    <th>Duration</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="yogaClassType in yogaClassTypes" :key="yogaClassType.yogaClassTypeId">
                    <td hidden="true">{{yogaClassType.yogaClassTypeId}}</td>
                    <td>{{yogaClassType.name}}</td>
                    <td>{{yogaClassType.description}}</td>
                    <td>{{yogaClassType.capacity}}</td>
                    <td>{{yogaClassType.duration}}</td>
                    <td><button type="button" class="btn btn-danger" @click="deleteYogaClassType(yogaClassType.yogaClassTypeId)">Delete</button></td>
                </tr>
            </tbody>
        </table>
        <ModalComp @close="toggleModal" :modalActive="modalActive">
            <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <form v-on:submit.prevent="onCreatePost">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLongTitle">Create Yoga Class Type</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">

                                <div class="form-group">
                                    <label>Name</label>
                                    <input type="text" class="form-control" v-model="form.name" />
                                </div>
                                <div class="form-group">
                                    <label>Description</label>
                                    <input type="text" class="form-control" v-model="form.description" />
                                </div>
                                <div class="form-group">
                                    <label>Capacity</label>
                                    <input type="text" class="form-control" v-model="form.capacity" />
                                </div>
                                <div class="form-group">
                                    <label>Duration</label>
                                    <input type="text" class="form-control" v-model="form.duration" />
                                </div>
                            </div>
                            <div class="modal-footer form-group">
                                <button type="submit" class="btn btn-primary">Save</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ModalComp>

    </div>

</template>
<script>
    import axios from 'axios';
    import authHeader from '../services/auth-header';

    export default {
        name: "YogaClassTypeComp",
        data() {
            return {
                form: {
                    name: "",
                    description: "",
                    capacity: "",
                    duration: "",
                },
                yogaClassTypes: []
            }
        },
        components: {
        },
        mounted() {
            axios
                .get("api/yogaclasstypes", { headers: authHeader() })
                .then((response) => {
                    this.yogaClassTypes = response.data;
                });
        },
        methods: {
            onCreatePost() {
                axios
                    .post("api/yogaclasstypes", this.form, { headers: authHeader() })
                    .then(response => {
                        console.log(response);

                    });
            },
            async deleteYogaClassType(id){
                await axios
                        .delete("api/yogaclasstypes/" + id, { headers: authHeader() })
                        .then(response => {
                            const index = this.yogaClassTypes.findIndex(yogaClassType => yogaClassType.yogaClassTypeId === id)
                            if(~index) this.yogaClassTypes.splice(index,1)
                        console.log(response)
                        });
            }
        }
    }
</script>
<style>
</style>