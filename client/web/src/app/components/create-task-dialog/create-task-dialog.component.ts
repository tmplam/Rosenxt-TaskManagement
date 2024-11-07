import { Component, inject, model, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckbox } from '@angular/material/checkbox';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  MatDialogRef,
  MatDialogContent,
  MatDialogActions,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { TasksService } from '../../services/tasks.service';
import { MatSnackBar } from '@angular/material/snack-bar';

// export interface CreateTaskDialogData {
//   title: string;
//   dueDate: Date;
//   remindBeforeDeadlineByMinutes?: number;
// }

@Component({
  selector: 'app-create-task-dialog',
  standalone: true,
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatCheckbox,
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './create-task-dialog.component.html',
  styleUrl: './create-task-dialog.component.css',
})
export class CreateTaskDialogComponent {
  private readonly _dialogRef = inject(MatDialogRef<CreateTaskDialogComponent>);
  private readonly _tasksService = inject(TasksService);
  private readonly _snackBar = inject(MatSnackBar);
  // readonly data = inject<CreateTaskDialogData>(MAT_DIALOG_DATA);

  isRemind = signal(false);

  createTaskForm = new FormGroup({
    title: new FormControl<string>('', [Validators.required]),
    dueDate: new FormControl<Date | null>(null, [Validators.required]),
    dueTime: new FormControl<string>('00:00', [Validators.required]),
    remindBeforeDeadlineByMinutes: new FormControl<number | null>(null, [
      Validators.min(0),
    ]),
  });

  onCancelClick() {
    this._dialogRef.close();
  }

  onSubmitCreateTask() {
    this.createTaskForm.markAllAsTouched();
    if (this.createTaskForm.valid) {
      const dueDate = this.createTaskForm.value.dueDate;
      const [hour, min] = this.createTaskForm.value.dueTime!.split(':');
      dueDate?.setHours(Number.parseInt(hour), Number.parseInt(min));

      const addTaskBody = {
        title: this.createTaskForm.value.title!,
        dueDate: dueDate!,
        remindBeforeDeadlineByMinutes:
          this.createTaskForm.value.remindBeforeDeadlineByMinutes,
      };

      this._tasksService.createTask(addTaskBody).subscribe((res) => {
        if (res && res.id) {
          this._snackBar.open('Add task successfully', 'OK');
          this._dialogRef.close(res);
        }
      });
    }
  }

  onRemindChange() {
    this.isRemind.update((prev) => !prev);
  }
}
