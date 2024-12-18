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
import { SignUpBody } from '../../models/interfaces/auth.interface';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    RouterModule,
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  private readonly _snackBar = inject(MatSnackBar);
  private readonly _authService = inject(AuthService);
  private readonly _router = inject(Router);

  signUpForm = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    name: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });
  isSubmitting = signal(false);

  passwordMatchValidator(g: FormGroup) {
    return g.get('password')?.value === g.get('confirmPassword')?.value
      ? null
      : { mismatch: true };
  }

  onSubmitSignUp() {
    if (this.signUpForm.valid) {
      const signUpBody: SignUpBody = {
        email: this.signUpForm.value.email!,
        name: this.signUpForm.value.name!,
        password: this.signUpForm.value.password!,
      };

      this.signUpForm.disable();
      this.isSubmitting.set(true);

      this._authService
        .signUp(signUpBody)
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
          if (res) {
            this._snackBar.open('Sign up successfully', 'OK');
            this._router.navigateByUrl('/login');
          }
          this.signUpForm.enable();
          this.isSubmitting.set(false);
        });
    }
  }
}
