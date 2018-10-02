import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'users/';

constructor(private http: HttpClient) { }

  getUsers(name?): Observable<User[]> {
    if (!name) {
      name = '';
    }
    return this.http.get<User[]>(this.baseUrl + name);
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'GetUser/' + id);
  }

  inviteUser(user) {
    return this.http.post(this.baseUrl + 'InviteUser', user);
  }
}
