import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { LoginBody, LoginResponse } from '../models/interfaces/auth.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(data: LoginBody): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.API_URL}auth/login`,
      data,
      { withCredentials: true }
    );
  }
}
