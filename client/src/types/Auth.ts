// 🔐 Request DTOs
export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  username: string;
  email: string;
  password: string;
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