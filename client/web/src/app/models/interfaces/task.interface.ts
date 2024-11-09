import { User } from './user.interface';

export interface Task {
  id: string;
  title: string;
  isCompleted: boolean;
  remindBeforeDeadlineByMinutes?: number;
  user?: User;
  dueDate: Date;
  modifiedAt: Date;
}
