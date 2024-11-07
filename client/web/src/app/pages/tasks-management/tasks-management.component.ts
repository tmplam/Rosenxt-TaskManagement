import { Component, inject, OnInit, signal } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Task } from '../../models/interfaces/task.interface';
import { TasksService } from '../../services/tasks.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

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
    DatePipe,
    ReactiveFormsModule,
  ],
  templateUrl: './tasks-management.component.html',
  styleUrl: './tasks-management.component.css',
})
export class TasksManagementComponent implements OnInit {
  private readonly _router = inject(Router);
  private readonly _tasksService = inject(TasksService);

  taskList = signal<Task[]>([]);
  taskStatusControl = new FormControl('all');

  ngOnInit(): void {
    this.getUserTaskList();
    this.taskStatusControl.valueChanges.subscribe((newStatus) =>
      this.getUserTaskList(newStatus)
    );
  }

  getUserTaskList(statusValue?: string | null) {
    let completeStatus: boolean | null = null;
    if (statusValue == 'completed') completeStatus = true;
    if (statusValue == 'incompleted') completeStatus = false;

    this._tasksService.getUserTasks(completeStatus).subscribe((res) => {
      if (res) {
        this.taskList.set(res.tasks);
      }
    });
  }
}
