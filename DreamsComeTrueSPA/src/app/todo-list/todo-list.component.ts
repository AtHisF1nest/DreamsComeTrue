import { Component, OnInit } from '@angular/core';
import { TodosService } from '../_services/todos.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  todoList: any;

  constructor(private todosService: TodosService) { }

  ngOnInit() {
    // this.getItems();
  }

  getItems() {
    this.todoList = this.todosService.getItems();
    console.log(this.todoList);
  }

}
