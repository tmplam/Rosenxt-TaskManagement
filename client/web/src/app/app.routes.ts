import { Routes } from '@angular/router';
import { TasksManagementComponent } from './pages/tasks-management/tasks-management.component';
import { LoginComponent } from './pages/login/login.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'sign-up',
    title: 'Sign up',
    component: SignUpComponent,
  },
  {
    path: 'login',
    title: 'Login',
    component: LoginComponent,
  },
  {
    path: 'tasks',
    title: 'Tasks',
    component: TasksManagementComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    redirectTo: '/login',
  },
];
