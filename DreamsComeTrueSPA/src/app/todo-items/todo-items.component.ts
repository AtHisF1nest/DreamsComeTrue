import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-todo-items',
  templateUrl: './todo-items.component.html',
  styleUrls: ['./todo-items.component.css']
})
export class TodoItemsComponent implements OnInit {

  todoItems: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getTodoItems();
  }

  getTodoItems() {
    this.http.get('http://localhost:5000/api/todos/GetItems').subscribe(result => {
      this.todoItems = result;
    }, error => {
      console.log(error);
    });
  }

}
