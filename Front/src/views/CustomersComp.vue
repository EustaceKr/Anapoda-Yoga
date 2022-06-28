<template>
    
    <h1>lelele</h1>
    <h1>lelele</h1>
    <h1>lelele</h1>
    <h1>lelele</h1>
    {{customers}}
</template>

<script>
let customers;
const API_URL = 'https://localhost:7198/api/customers'

export default {
  name: "CustomersComp",
  components: {},
  data(){
    return {
        customers,
    }
  },
  created(){
    this.fetchData()
  },
  methods:{
    async fetchData() {
        this.customers = await (await fetch(API_URL)).json()
    }
  }
}

</script>

<style lang="scss" scoped>

</style>