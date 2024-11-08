import { Component, inject, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { LoginBody } from '../../models/interfaces/auth.interface';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    RouterModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private readonly _snackBar = inject(MatSnackBar);
  private readonly _authService = inject(AuthService);
  private readonly _router = inject(Router);

  loginForm = new FormGroup({
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });
  isSubmitting = signal(false);

  onSubmitLogin() {
    if (this.loginForm.valid) {
      const loginBody: LoginBody = {
        email: this.loginForm.value.email!,
        password: this.loginForm.value.password!,
      };

      this.loginForm.disable();
      this.isSubmitting.set(true);

      this._authService
        .login(loginBody)
        .pipe(
          catchError((error) => {
            this._snackBar.open(
              error.error.detail ?? 'Invalid credentials',
              'OK'
            );
            return of(null);
          })
        )
        .subscribe((res) => {
          if (res && res.token) {
            localStorage.setItem('token', res.token);
            this._snackBar.open('Welcome', 'OK');
            this._router.navigateByUrl('/tasks');
          }
          this.loginForm.enable();
          this.isSubmitting.set(false);
        });
    }
  }
}
