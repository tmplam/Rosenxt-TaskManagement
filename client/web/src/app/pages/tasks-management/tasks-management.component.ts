import { Component, inject, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-tasks-management',
  standalone: true,
  imports: [
    MatButtonToggleModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatCardModule,
    MatCheckboxModule,
    MatMenuModule,
  ],
  templateUrl: './tasks-management.component.html',
  styleUrl: './tasks-management.component.css',
})
export class TasksManagementComponent implements OnInit {
  private readonly _router = inject(Router);
  private readonly _authService = inject(AuthService);

  ngOnInit(): void {}
}
