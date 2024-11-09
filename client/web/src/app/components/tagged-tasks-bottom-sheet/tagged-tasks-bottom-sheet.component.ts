import { Component, inject, OnInit, signal } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Task } from '../../models/interfaces/task.interface';
import { TasksService } from '../../services/tasks.service';
import { DatePipe } from '@angular/common';

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
  private readonly _tasksService = inject(TasksService);

  isFetchingTaggedTask = signal(false);
  taggedTasks = signal<Task[]>([]);

  ngOnInit(): void {
    this.getTaggedTaskList();
  }

  getTaggedTaskList() {
    this.isFetchingTaggedTask.set(true);
    this._tasksService.getTaggedTasks().subscribe((res) => {
      if (res && res.taggedTasks) {
        console.log(res.taggedTasks);
        this.taggedTasks.set(res.taggedTasks);
      }
      this.isFetchingTaggedTask.set(false);
    });
  }

  openLink(event: MouseEvent): void {
    this._bottomSheetRef.dismiss();
    event.preventDefault();
  }
}
