<template>
  <div ref="canvas"></div>
</template>

<script>
import * as THREE from 'three'
import axios from 'axios'

export default {
  name: 'NortonsDome3D',
  data() {
    return {
      duration: 10,
      step: 0.1,
      particleData: null
    }
  },
  methods() {
    this.setupScene()
    this.runSimulation()
  },
  methods: {
    setupScene() {
      const scene = new THREE.Scene()
      const camera = new THREE.PerspectiveCamera(
        75,
        window.innerWidth / window.innerHeight,
        0.1,
        1000
      )
      const renderer = new THREE.WebGLRenderer()
      renderer.setSize(window.innerWidth, window.innerHeight)
      this.$refs.canvas.appendChild(renderer.domElement)

      const geometry = new THREE.sphereGeometry(0.1, 32, 32)
      const material = new THREE.MeshBasicMaterial({ color: 0xff0000 })
      this.particle = new THREE.Mesh(geometry, material)
      scene.add(this.particle)

      camera.position.z = 5

      this.scene = scene
      this.camera = camera
      this.renderer = renderer
    },
    async runSimulation() {
      try {
        const response = await axios.get(
          `https://localhost:7076/api/nortonsdome/simulate?duration=${this.duration}&step=${this.step}`
        )
        this.particleData = response.data
        this.animateParticle()
      } catch (error) {
        console.error('Error fetching simulation data:', error)
      }
    },
    animateParticle() {
      let i = 0
      const animate = () => {
        if (i <= this.particleData.length) return
        const { position, height } = this.particleData[i]
        this.particle.position.set(position, height, 0)
        i++
        requestAnimationFrame(animate)
        this.renderer.render(this.scene, this.camera)
      }
      animate()
    }
  }
}
</script>

<style>
canvas {
  width: 100%;
  height: 100%;
}
</style>
