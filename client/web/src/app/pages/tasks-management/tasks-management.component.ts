import { Component, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { CreateTaskDialogComponent } from '../../components/create-task-dialog/create-task-dialog.component';
import { UpdateTaskDialogComponent } from '../../components/update-task-dialog/update-task-dialog.component';
import { TasksService } from '../../services/tasks.service';
import { AuthService } from '../../services/auth.service';
import { Task } from '../../models/interfaces/task.interface';

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
  private readonly _snackBar = inject(MatSnackBar);
  private readonly _router = inject(Router);
  private readonly _dialog = inject(MatDialog);
  private readonly _tasksService = inject(TasksService);
  private readonly _authService = inject(AuthService);

  taskList = signal<Task[]>([]);
  isFetchingTaskList = signal(false);
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
    if (statusValue == 'incomplete') completeStatus = false;

    this.isFetchingTaskList.set(true);
    this._tasksService.getUserTasks(completeStatus).subscribe((res) => {
      if (res) {
        this.taskList.set(res.tasks);
      }
      this.isFetchingTaskList.set(false);
    });
  }

  toggleCompleteTask(task: Task) {
    this._tasksService.toggleCompleteTask(task.id).subscribe((res) => {
      if (res && res.id) {
        const taskIndex = this.taskList().findIndex((t) => t.id === task.id);
        if (taskIndex !== -1) {
          if (this.taskStatusControl.value !== 'all') {
            this.taskList.update((tasks) =>
              tasks.filter((t) => t.id !== task.id)
            );
          } else {
            this.taskList.update((tasks) => {
              tasks[taskIndex].isCompleted = !tasks[taskIndex].isCompleted;
              return tasks;
            });
          }
          this._snackBar.open('Update successfully', 'OK');
        }
      }
    });
  }

  deleteTask(task: Task) {
    this._tasksService.deleteTask(task.id).subscribe((res) => {
      if (res && res.id) {
        const taskIndex = this.taskList().findIndex((t) => t.id === task.id);
        if (taskIndex !== -1) {
          this.taskList.update((tasks) =>
            tasks.filter((t) => t.id !== task.id)
          );
          this._snackBar.open('Deleted task', 'OK');
        }
      }
    });
  }

  openAddTaskDialog() {
    const dialogRef = this._dialog.open(CreateTaskDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getUserTaskList(this.taskStatusControl.value);
      }
    });
  }

  openUpdateTaskDialog(task: Task) {
    task.dueDate = new Date(task.dueDate);
    const dialogRef = this._dialog.open(UpdateTaskDialogComponent, {
      data: task,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getUserTaskList(this.taskStatusControl.value);
      }
    });
  }

  logout() {
    this._authService.removeToken();
    this._router.navigateByUrl('/login');
  }
}
