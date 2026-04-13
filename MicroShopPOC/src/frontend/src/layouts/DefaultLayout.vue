<script setup lang="ts">
import { useAuthStore } from '../stores/authStore'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <div class="min-h-screen bg-gray-50 flex flex-col">
    <nav class="bg-white shadow px-6 py-3 flex items-center justify-between">
      <span class="text-lg font-bold text-blue-700">MicroShop POC</span>
      <div class="flex items-center gap-4">
        <span class="text-sm text-gray-600">{{ authStore.username }}</span>
        <button
          @click="logout"
          class="px-3 py-1 text-sm bg-red-500 text-white rounded hover:bg-red-600 transition-colors"
        >
          Logout
        </button>
      </div>
    </nav>
    <main class="flex-1 p-6">
      <RouterView />
    </main>
  </div>
</template>
