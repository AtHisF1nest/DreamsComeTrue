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
  currentUser: User;

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

  editAvatar(file: File): any {
    const formData: FormData = new FormData();
    formData.append('fileKey', file, file.name);
    return this.http.post(this.baseUrl + 'EditAvatar', formData);
  }

  editUser(user: User) {
    return this.http.post(this.baseUrl + 'EditUser', user);
  }

  getCurrentUser(): User {
    if (!this.currentUser) {
      this.http.get<User>(this.baseUrl + 'GetCurrentUser').subscribe(res => {
        this.currentUser = res;
      });
      return this.currentUser;
    }
  }
}
