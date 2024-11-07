export interface SignUpBody {
  email: string;
  name: string;
  password: string;
}

export interface LoginBody {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}
