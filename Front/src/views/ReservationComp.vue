<template>
        <br/>
    <br/>
    <br/>
    <br/>
<vue-cal 
    class="vuecal--blue-theme" 
    locale="el"
    active-view="week"
    :disable-views="['day','month','years', 'year']"
    hide-view-selector
    :hide-weekdays="[7]"
    :time-from="10 * 60"
    :time-to="23 * 60"
    >
</vue-cal>
</template>
<script>
import VueCal from 'vue-cal'
import '../../node_modules/vue-cal/dist/vuecal.css'

export default{
    name: "ReservationComp",
    components:{ VueCal }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.vuecal {height: 90vh;}
</style>