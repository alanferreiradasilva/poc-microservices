import { apiGateway } from './apiGateway'
import type { TokenResponse } from '../models/User'

export const authService = {
  login: (username: string, password: string) =>
    apiGateway.post<TokenResponse>('/api/auth/login', { username, password }),

  register: (username: string, password: string) =>
    apiGateway.post<TokenResponse>('/api/auth/register', { username, password }),
}
