import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Todo } from '../_models/todo';
import { Category } from '../_models/category';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class TodosService {

  baseUrl = environment.apiUrl + 'todos/';

  constructor(private http: HttpClient) { }

  getItems(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetItems', httpOptions);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'GetCategories', httpOptions);
  }

}
