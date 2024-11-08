import { Component, inject, model, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  MatDialogRef,
  MatDialogContent,
  MatDialogActions,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckbox } from '@angular/material/checkbox';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TasksService } from '../../services/tasks.service';

export interface UpdateTaskDialogData {
  id: string;
  title: string;
  isCompleted: boolean;
  remindBeforeDeadlineByMinutes: number | null;
  dueDate: Date;
}

@Component({
  selector: 'app-update-task-dialog',
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
  templateUrl: './update-task-dialog.component.html',
  styleUrl: './update-task-dialog.component.css',
})
export class UpdateTaskDialogComponent {
  private readonly _dialogRef = inject(MatDialogRef<UpdateTaskDialogComponent>);
  private readonly _tasksService = inject(TasksService);
  private readonly _snackBar = inject(MatSnackBar);
  readonly data = inject<UpdateTaskDialogData>(MAT_DIALOG_DATA);

  isRemind = signal(this.data.remindBeforeDeadlineByMinutes != null);
  isSubmitting = signal(false);

  updateTaskForm = new FormGroup({
    title: new FormControl<string>(this.data.title, [Validators.required]),
    dueDate: new FormControl<Date | null>(this.data.dueDate, [
      Validators.required,
    ]),
    dueTime: new FormControl<string>(this.formatTime(this.data.dueDate), [
      Validators.required,
    ]),
    remindBeforeDeadlineByMinutes: new FormControl<number | null>(
      this.data.remindBeforeDeadlineByMinutes,
      [Validators.min(0)]
    ),
  });

  private formatTime(date: Date): string {
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
  }

  onCancelClick() {
    this._dialogRef.close();
  }

  onSubmitCreateTask() {
    this.updateTaskForm.markAllAsTouched();

    if (this.updateTaskForm.valid) {
      const dueDate = this.updateTaskForm.value.dueDate;
      const [hour, min] = this.updateTaskForm.value.dueTime!.split(':');
      dueDate?.setHours(Number.parseInt(hour), Number.parseInt(min));

      const updateTaskBody = {
        id: this.data.id,
        title: this.updateTaskForm.value.title!,
        dueDate: dueDate!,
        remindBeforeDeadlineByMinutes: this.isRemind()
          ? this.updateTaskForm.value.remindBeforeDeadlineByMinutes
          : null,
      };

      this.updateTaskForm.disable();
      this.isSubmitting.set(true);

      this._tasksService.updateTask(updateTaskBody).subscribe((res) => {
        if (res && res.id) {
          this._snackBar.open('Saved task', 'OK');
          this._dialogRef.close(res);
        }
        this.updateTaskForm.enable();
        this.isSubmitting.set(false);
      });
    }
  }

  onRemindChange() {
    this.isRemind.update((prev) => !prev);
  }
}
