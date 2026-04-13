import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { authService } from '../services/authService'
import { jwtDecode } from 'jwt-decode'

interface JwtPayload {
  unique_name: string
  sub: string
}

const TOKEN_KEY = 'auth_token'

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(localStorage.getItem(TOKEN_KEY))
  const username = ref<string | null>(null)

  const isAuthenticated = computed(() => !!token.value)

  const hydrateFromToken = (t: string) => {
    try {
      const payload = jwtDecode<JwtPayload>(t)
      username.value = payload.unique_name
    } catch {
      username.value = null
    }
  }

  if (token.value) {
    hydrateFromToken(token.value)
  }

  const login = async (usernameInput: string, password: string): Promise<boolean> => {
    const { data, status } = await authService.login(usernameInput, password)
    if (status === 200 && data?.token) {
      token.value = data.token
      localStorage.setItem(TOKEN_KEY, data.token)
      hydrateFromToken(data.token)
      return true
    }
    return false
  }

  const logout = () => {
    token.value = null
    username.value = null
    localStorage.removeItem(TOKEN_KEY)
  }

  return { token, username, isAuthenticated, login, logout }
})
