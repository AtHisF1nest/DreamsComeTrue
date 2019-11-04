import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Todo } from '../_models/todo';
import { Category } from '../_models/category';
import { HistoryDto } from '../_models/historyDto';
import { EventItem } from '../_models/eventItem';

@Injectable({
  providedIn: 'root'
})
export class TodosService {

  baseUrl = environment.apiUrl + 'todos/';

  constructor(private http: HttpClient) { }

  getItems(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl);
  }

  getItemsByCategories(categories: Category[]): Observable<Todo[]> {
    return this.http.post<Todo[]>(this.baseUrl + 'GetTodosByCategories', categories);
  }

  getDreamsByCategories(categories: Category[]): Observable<Todo[]> {
    return this.http.post<Todo[]>(this.baseUrl + 'GetDreamsByCategories', categories);
  }

  getNotConnectedItems(categoryId): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetNotConnectedItems/' + categoryId);
  }

  getItem(id): Observable<Todo> {
    return this.http.get<Todo>(this.baseUrl + id);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'GetCategories');
  }

  getDreams(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetDreams');
  }

  getNotConnectedDreams(categoryId): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetNotConnectedDreams/' + categoryId);
  }

  getDream(id): Observable<Todo> {
    return this.http.get<Todo>(this.baseUrl + 'GetDream/' + id);
  }

  getDreamsCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'GetDreamsCategories');
  }

  getTodosConnections(id): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetTodosConnections/' + id);
  }

  getTodosDreamsConnections(id): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetTodosDreamsConnections/' + id);
  }

  getHistory(id): Observable<HistoryDto[]> {
    return this.http.get<HistoryDto[]>(this.baseUrl + 'GetHistoryOfTodo/' + id);
  }

  getDoneItems(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.baseUrl + 'GetDoneTodoItems');
  }

  getEvents(): Observable<EventItem[]> {
    return this.http.get<EventItem[]>(this.baseUrl + 'GetEvents');
  }

  addEvent(eventItem: EventItem): any {
    return this.http.post(this.baseUrl + 'AddEvent', eventItem);
  }

  modifyEvent(eventItem: EventItem) {
    return this.http.put(this.baseUrl + 'UpdateEvent', eventItem);
  }

  deleteEvent(eventItemId: number) {
    return this.http.delete(this.baseUrl + 'DeleteEvent/' + eventItemId);
  }

}
