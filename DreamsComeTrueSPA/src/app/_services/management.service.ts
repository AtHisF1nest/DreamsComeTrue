import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ManagementType } from '../_models/managementType';
import { Observable } from 'rxjs';
import { Todo } from '../_models/todo';
import { Category } from '../_models/category';

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
}
