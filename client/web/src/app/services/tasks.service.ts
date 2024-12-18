import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Task } from '../models/interfaces/task.interface';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  private readonly _http = inject(HttpClient);

  getUserTasks(isCompleted?: boolean | null): Observable<{ tasks: Task[] }> {
    let query = '';
    if (isCompleted != null) {
      query = `?isCompleted=${isCompleted}`;
    }
    return this._http.get<{ tasks: Task[] }>(
      `${environment.API_URL}tasks${query}`
    );
  }

  getTaggedTasks(): Observable<{ taggedTasks: Task[] }> {
    return this._http.get<{ taggedTasks: Task[] }>(
      `${environment.API_URL}tasks/tagged`
    );
  }

  getTaggedTasksCount(): Observable<{ count: number }> {
    return this._http.get<{ count: number }>(
      `${environment.API_URL}tasks/tagged/count`
    );
  }

  toggleCompleteTask(taskId: string): Observable<any> {
    return this._http.patch(
      `${environment.API_URL}tasks/${taskId}/toggle-complete`,
      {}
    );
  }

  createTask(task: {
    title: string;
    dueDate: Date;
    remindBeforeDeadlineByMinutes?: number | null;
  }): Observable<any> {
    return this._http.post(`${environment.API_URL}tasks`, task);
  }

  updateTask(task: {
    id: string;
    title: string;
    dueDate: Date;
    remindBeforeDeadlineByMinutes?: number | null;
  }): Observable<any> {
    return this._http.put(`${environment.API_URL}tasks`, task);
  }

  deleteTask(taskId: string): Observable<any> {
    return this._http.delete(`${environment.API_URL}tasks/${taskId}`);
  }

  tagUsersToTask(data: { taskId: string; emails: string[] }): Observable<any> {
    return this._http.post(`${environment.API_URL}tasks/tag-users`, data);
  }

  acceptTaggedTask(data: { taskId: string }): Observable<any> {
    return this._http.post(`${environment.API_URL}tasks/tagged/accept`, data);
  }

  declineTaggedTask(data: { taskId: string }): Observable<any> {
    return this._http.post(`${environment.API_URL}tasks/tagged/decline`, data);
  }
}
