import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TodosService {

  baseUrl = 'http://localhost:5000/api/todos/';

  constructor(private http: HttpClient) { }

  getItems() {
    return this.http.get(this.baseUrl + 'GetItems').pipe(
      map((response: any) => {
        console.log(response);
      })
    );
  }

}
