import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private readonly _http = inject(HttpClient);

  getEmailList(keyword: string): Observable<{ emailList: string[] }> {
    return this._http.get<{ emailList: string[] }>(
      `${environment.API_URL}users/email-search?keyword=${keyword}`
    );
  }
}
