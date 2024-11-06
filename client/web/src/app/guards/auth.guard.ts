import { inject, Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private readonly _authService = inject(AuthService);
  private readonly _router = inject(Router);

  canActivate(): boolean {
    if (this._authService.isLoggedIn()) {
      return true;
    } else {
      this._router.navigateByUrl('/login');
      return false;
    }
  }
}
