import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { LoginBody, LoginResponse } from '../models/interfaces/auth.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'token';

  private readonly _http = inject(HttpClient);

  login(data: LoginBody): Observable<LoginResponse> {
    return this._http.post<LoginResponse>(
      `${environment.API_URL}auth/login`,
      data
    );
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
