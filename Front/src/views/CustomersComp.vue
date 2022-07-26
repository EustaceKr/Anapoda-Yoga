<template>
    <br/>
    <br/>
    <br/>
    <br/>
    <div v-for="customer in customers" :key="customer.id">
      <p hidden="true">{{customer.id}}</p>
      <h2>Full Name: {{customer.firstName}} {{customer.lastName}}</h2>
      <p>Created at: {{customer.createdDate}}</p>
      <p>Updated at: {{customer.updatedDate}}</p>
    </div>
</template>

<script>
import axios from 'axios'

export default {
  name: "CustomersComp",
  data(){
    return{
      customers: []
    }
  },
  mounted(){
    axios
      .get('api/customers')
      .then((response) => {
    this.customers = response.data})
  },
}
</script>


<style lang="scss" scoped>
</style>