import { Component, inject } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-tagged-tasks-bottom-sheet',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatTooltipModule],
  templateUrl: './tagged-tasks-bottom-sheet.component.html',
  styleUrl: './tagged-tasks-bottom-sheet.component.css',
})
export class TaggedTasksBottomSheetComponent {
  private readonly _bottomSheetRef =
    inject<MatBottomSheetRef<TaggedTasksBottomSheetComponent>>(
      MatBottomSheetRef
    );

  openLink(event: MouseEvent): void {
    this._bottomSheetRef.dismiss();
    event.preventDefault();
  }
}
