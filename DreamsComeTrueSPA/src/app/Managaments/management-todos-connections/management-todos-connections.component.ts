import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { Category } from '../../_models/category';

@Component({
  selector: 'app-management-todos-connections',
  templateUrl: './management-todos-connections.component.html',
  styleUrls: ['./management-todos-connections.component.css']
})
export class ManagementTodosConnectionsComponent implements OnInit {

  todoList: Todo[];

  categoryList: Category[];

  constructor() { }

  ngOnInit() {
  }

}
