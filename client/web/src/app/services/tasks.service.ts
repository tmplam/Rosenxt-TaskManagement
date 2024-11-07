import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
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
    return this._http.get<{ tasks: Task[] }>(`${environment.API_URL}tasks${query}`);
  }
}
