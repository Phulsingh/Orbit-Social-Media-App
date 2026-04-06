import { apiService } from "./apiService";

// 🔐 Request DTOs
export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResponse {
  id : number;
  token: string;
  expiration: string;
  userName: string;
  email: string;
  roles: string[];
}

export interface forgotPasswordRequest {
  email: string;
}

export interface resetPasswordRequest{
  token: string;
  newPassword: string;
  confirmPassword: string;
}

// 👇 Hook-based service (same pattern as your reference)
export const useUserService = () => {
  const api = apiService;

  const userService = {
    login: async (  payload: LoginRequest): Promise<AuthResponse> => {
      const response = await api.post("auth/login", payload);
      return response as AuthResponse;
    },

    register: async (
      payload: RegisterRequest,
    ): Promise<AuthResponse> => {
      const response = await api.post("auth/register", payload);
      return response as AuthResponse;
    },
   
     forgotPassword: async (payload: forgotPasswordRequest): Promise<void> => {
      const response = await api.post("Auth/forgot-password", payload);
      return response as void;
    },

    resetPassword: async (payload: resetPasswordRequest): Promise<void> => {
      const response = await api.post("Auth/reset-password", payload);
      return response as void;
    },
    // 🚪 LOGOUT
    logout: () => {
      localStorage.removeItem("token");
    },
  };

  return userService;
};