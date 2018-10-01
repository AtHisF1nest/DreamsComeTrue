import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ManagementType } from '../_models/managementType';
import { Observable } from 'rxjs';
import { Todo } from '../_models/todo';
import { Category } from '../_models/category';
import { TodoConnection } from '../_models/todoConnection';

@Injectable({
  providedIn: 'root'
})
export class ManagementService {

  baseUrl = environment.apiUrl + 'management/';

  constructor(private http: HttpClient) { }

  getManagementTypes(): Observable<ManagementType[]> {
    return this.http.get<ManagementType[]>(this.baseUrl);
  }

  addTodo(todoModel: any) {
    return this.http.post(this.baseUrl + 'AddTodo', todoModel);
  }

  deleteTodo(id) {
    return this.http.delete(this.baseUrl + 'DeleteTodo/' + id);
  }

  addDream(dreamModel: any) {
    return this.http.post(this.baseUrl + 'AddDream', dreamModel);
  }

  deleteDream(id) {
    return this.http.delete(this.baseUrl + 'DeleteDream/' + id);
  }

  addCategory(categoryModel: Category) {
    return this.http.post(this.baseUrl + 'AddCategory', categoryModel);
  }

  deleteCategory(id) {
    return this.http.delete(this.baseUrl + 'DeleteCategory/' + id);
  }

  addDreamCategory(categoryModel: Category) {
    return this.http.post(this.baseUrl + 'AddDreamCategory', categoryModel);
  }

  deleteDreamCategory(id) {
    return this.http.delete(this.baseUrl + 'DeleteDreamCategory/' + id);
  }

  connectItems(connection: TodoConnection) {
    return this.http.post(this.baseUrl + 'ConnectItems', connection);
  }

  unConnectItems(connection: TodoConnection) {
     return this.http.post(this.baseUrl + 'UnConnectItems', connection);
  }

  realizeTodo(todoItem: any) {
    return this.http.post(this.baseUrl + 'RealizeTodo', todoItem);
  }

  deleteHistory(id) {
    return this.http.delete(this.baseUrl + 'DeleteHistory/' + id);
  }

  editItem(item) {
    return this.http.put(this.baseUrl + 'EditItem', item);
  }
}
