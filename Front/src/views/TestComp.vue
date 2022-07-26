<template>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <br/>
    <div>
        {{testData}}
    </div>
</template>

<script>
import axios from 'axios'
import authHeader from '../services/auth-header';

export default {
  name: "TestComp",
  data(){
    return{
      testData: []
    }
  },
  mounted(){
    axios
      .get('api/test', { headers: authHeader() })
      .then((response) => {
    this.testData = response.data})
  },
}
</script>


<style lang="scss" scoped>
</style>