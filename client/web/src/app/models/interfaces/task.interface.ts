export interface Task {
  id: string;
  title: string;
  isCompleted: boolean;
  remindBeforeDeadlineByMinutes: number;
  dueDate: Date;
  modifiedAt: Date;
}
