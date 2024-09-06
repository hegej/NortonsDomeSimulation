<template>
  <div class="nortons-dome-simulation">
    <h1>Norton's Dome Simulation</h1>

    <NortonsDomeSimulation />
  </div>
</template>

<script>
import NortonsDomeSimulation from '@/components/NortonsDomeSimulation.vue'

export default {
  name: 'NortonsDomeSimulation',
  components: {
    NortonsDomeSimulation
  }
}
</script>

<style scoped>
.norton-dome-simulation {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #2c3e50;
  color: #ecf0f1;
}

h1 {
  margin-bottom: 20px;
}
</style>
