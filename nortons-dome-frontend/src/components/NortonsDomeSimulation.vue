<template>
  <div>
    <h1>Norton's Dome Simulation</h1>
    <div>
      <label for="duration">Duration:</label>
      <input id="duration" v-model.number="duration" type="number" step="0.1" min="0" />
      <label for="step">Step:</label>
      <input id="step" v-model.number="step" type="number" step="0.01" min="0.01" />
      <button @click="runSimulation">Run Simulation</button>
    </div>

    <Line v-if="chartData" :chart-data="chartData" :options="chartOptions" />

    <NortonsDome3D />
  </div>
</template>

<script>
import { ref } from 'vue'
import axios from 'axios'
import { LineChart } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  LinearScale,
  PointElement,
  CategoryScale
} from 'chart.js'
import NortonsDome3D from './NortonsDome3D.vue'

ChartJS.register(Title, Tooltip, Legend, LineElement, LinearScale, PointElement, CategoryScale)

export default {
  components: { LineChart, NortonsDome3D },
  setup() {
    const duration = ref(10)
    const step = ref(0.1)
    const chartData = ref(null)
    const chartOptions = {
      responsive: true,
      maintainAspectRatio: false
    }

    const runSimulation = async () => {
      try {
        const response = await axios.get(
          `https://localhost:7076/api/nortonsdome/simulate?duration=${duration.value}&step=${step.value}`
        )
        const data = response.data
        chartData.value = {
          labels: data.map((d) => d.time),
          datasets: [
            {
              label: 'Position',
              data: data.map((d) => d.position),
              borderColor: 'rgb(75, 192, 192)',
              tension: 0.1
            },
            {
              label: 'Height',
              data: data.map((d) => d.height),
              borderColor: 'rgb(255, 99, 132)',
              tension: 0.1
            }
          ]
        }
      } catch (error) {
        console.error('Error fetching simulation data:', error)
      }
    }

    return { duration, step, chartData, chartOptions, runSimulation }
  }
}
</script>
