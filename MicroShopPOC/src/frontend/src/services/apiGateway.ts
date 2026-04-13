import { useAuthStore } from '../stores/authStore'

const BASE_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5000'

const request = async <T>(path: string, options: RequestInit = {}): Promise<{ data: T | null; status: number }> => {
  const authStore = useAuthStore()

  const headers: Record<string, string> = {
    'Content-Type': 'application/json',
    ...(options.headers as Record<string, string>),
  }

  if (authStore.token) {
    headers['Authorization'] = `Bearer ${authStore.token}`
  }

  const response = await fetch(`${BASE_URL}${path}`, { ...options, headers })

  if (response.status === 204) return { data: null, status: response.status }

  const data = response.headers.get('content-type')?.includes('application/json')
    ? await response.json()
    : null

  return { data, status: response.status }
}

export const apiGateway = {
  post: <T>(path: string, body: unknown) =>
    request<T>(path, { method: 'POST', body: JSON.stringify(body) }),
  get: <T>(path: string) =>
    request<T>(path, { method: 'GET' }),
}
