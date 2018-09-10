import { Component, OnInit } from '@angular/core';
import { TodosService } from '../../_services/todos.service';
import { Todo } from '../../_models/todo';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  todoList: Todo[];

  constructor(private todosService: TodosService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadItems();
  }

  loadItems() {
    this.todosService.getItems().subscribe((todos: Todo[]) => {
      this.todoList = todos;
    }, error => {
      this.alertify.error(error);
    });
  }

}
