import { Component, inject, Injectable, OnInit, signal } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Task } from '../../models/interfaces/task.interface';
import { TasksService } from '../../services/tasks.service';
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-tagged-tasks-bottom-sheet',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatTooltipModule, DatePipe],
  templateUrl: './tagged-tasks-bottom-sheet.component.html',
  styleUrl: './tagged-tasks-bottom-sheet.component.css',
})
export class TaggedTasksBottomSheetComponent implements OnInit {
  private readonly _bottomSheetRef =
    inject<MatBottomSheetRef<TaggedTasksBottomSheetComponent>>(
      MatBottomSheetRef
    );
  private readonly _snackBar = inject(MatSnackBar);
  private readonly _tasksService = inject(TasksService);
  private readonly _dataService = inject(TaggedTasksBottomSheetDataService);

  isFetchingTaggedTask = signal(false);
  taggedTasks = signal<Task[]>([]);
  isSubmittingList = signal<string[]>([]);

  ngOnInit(): void {
    this.getTaggedTaskList();
  }

  getTaggedTaskList() {
    this.isFetchingTaggedTask.set(true);
    this._tasksService.getTaggedTasks().subscribe((res) => {
      if (res && res.taggedTasks) {
        this.taggedTasks.set(res.taggedTasks);
      }
      this.isFetchingTaggedTask.set(false);
    });
  }

  openLink(event: MouseEvent): void {
    this._bottomSheetRef.dismiss();
    event.preventDefault();
  }

  acceptTaggedTask(task: Task) {
    this.isSubmittingList.update((prev) => {
      prev.push(task.id);
      return prev;
    });
    this._tasksService
      .acceptTaggedTask({ taskId: task.id })
      .subscribe((res) => {
        if (res && res.taskId) {
          const newTask: Task = {
            id: res.taskId,
            title: task.title,
            dueDate: task.dueDate,
            isCompleted: false,
          };
          this.taggedTasks.update((prev) =>
            prev.filter((t) => t.id != task.id)
          );
          this._dataService.sendData(newTask);
          this._snackBar.open('Accepted task', 'OK');
        }
        this.isSubmittingList.update((prev) =>
          prev.filter((id) => id != task.id)
        );
      });
  }

  declineTaggedTask(task: Task) {
    this.isSubmittingList.update((prev) => {
      prev.push(task.id);
      return prev;
    });
    this._tasksService
      .declineTaggedTask({ taskId: task.id })
      .subscribe((res) => {
        if (res && res.taskId) {
          this.taggedTasks.update((prev) =>
            prev.filter((t) => t.id != task.id)
          );
          this._snackBar.open('Declined task', 'OK');
        }
        this.isSubmittingList.update((prev) =>
          prev.filter((id) => id != task.id)
        );
      });
  }
}

@Injectable({ providedIn: 'root' })
export class TaggedTasksBottomSheetDataService {
  private dataSubject = new Subject<Task>();
  data$ = this.dataSubject.asObservable();

  sendData(data: Task) {
    this.dataSubject.next(data);
  }
}
