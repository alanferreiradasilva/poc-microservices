<script setup lang="ts">
import { useAuthStore } from '../stores/authStore'

const authStore = useAuthStore()
</script>

<template>
  <div>
    <h1 class="text-3xl font-bold text-gray-800 mb-2">Welcome, {{ authStore.username }}!</h1>
    <p class="text-gray-500">Use the navigation to manage products, sales, and users.</p>
  </div>
</template>
