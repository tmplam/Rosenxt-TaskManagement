import { Component, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { UsersService } from '../../services/users.service';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  startWith,
  switchMap,
} from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AsyncPipe, CommonModule, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Task } from '../../models/interfaces/task.interface';
import { TasksService } from '../../services/tasks.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tag-friends-dialog',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatDialogContent,
    MatDialogActions,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    CommonModule,
    NgIf,
    AsyncPipe,
  ],
  templateUrl: './tag-friends-dialog.component.html',
  styleUrl: './tag-friends-dialog.component.css',
})
export class TagFriendsDialogComponent {
  private readonly _dialogRef = inject(MatDialogRef<TagFriendsDialogComponent>);
  private readonly _usersService = inject(UsersService);
  private readonly _tasksService = inject(TasksService);
  private readonly _snackBar = inject(MatSnackBar);
  readonly data = inject<Task>(MAT_DIALOG_DATA);

  selectedEmails = signal<string[]>([]);
  searchEmailKeyword = new FormControl('');
  isSubmitting = signal(false);

  emailSearchResult: Observable<{
    isLoading: boolean;
    emailList: string[];
    error: any;
  }> = this.searchEmailKeyword.valueChanges.pipe(
    debounceTime(400),
    distinctUntilChanged(),
    filter((keyword) => !!keyword?.trim()),
    switchMap((keyword) =>
      this._usersService.getEmailList(keyword!).pipe(
        map((response) => ({
          isLoading: false,
          emailList: response.emailList,
          error: null,
        })),
        catchError((error) => of({ error, isLoading: false, emailList: [] })),
        startWith({ error: null, isLoading: true, emailList: [] })
      )
    )
  );

  onCancelClick() {
    this._dialogRef.close();
  }

  onEmailSelect(email: string) {
    this.selectedEmails.update((prev) => {
      const exist = prev.find((e) => e == email);
      if (!exist) {
        prev.push(email);
      }
      return prev;
    });
    this.searchEmailKeyword.reset();
  }

  onRemoveEmail(email: string) {
    this.selectedEmails.update((prev) => prev.filter((e) => e != email));
  }

  onSubmitTag() {
    if (this.selectedEmails().length > 0) {
      this.isSubmitting.set(true);
      this._tasksService
        .tagUsersToTask({
          taskId: this.data.id,
          emails: this.selectedEmails(),
        })
        .subscribe((res) => {
          if (res && res.taskId) {
            this._snackBar.open('Tag successfully', 'OK');
            this._dialogRef.close(res);
          }
          this.isSubmitting.set(false);
        });
    }
  }
}
