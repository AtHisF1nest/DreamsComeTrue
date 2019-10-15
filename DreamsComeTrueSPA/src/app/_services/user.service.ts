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

  getInvitations(userId): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'GetInvitationsForUser/' + userId);
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'GetUser/' + id);
  }

  inviteUser(user) {
    return this.http.post(this.baseUrl + 'InviteUser', user);
  }

  unInviteUser(user) {
    return this.http.delete(this.baseUrl + 'UnInviteUser/' + user.id);
  }

  editUser(user: User) {
    return this.http.post(this.baseUrl + 'EditUser', user);
  }

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'GetCurrentUser');
  }

  acceptInvite(userId) {
    return this.http.put(this.baseUrl + 'AcceptInvite/' + userId, {});
  }

  leavePair() {
    return this.http.delete(this.baseUrl + 'LeavePair');
  }
}
