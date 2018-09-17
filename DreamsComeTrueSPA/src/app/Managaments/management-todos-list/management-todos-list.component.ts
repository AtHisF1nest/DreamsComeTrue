import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Todo } from '../../_models/todo';

@Component({
  selector: 'app-management-todos-list',
  templateUrl: './management-todos-list.component.html',
  styleUrls: ['./management-todos-list.component.css']
})
export class ManagementTodosListComponent implements OnInit {

  @Input() todoList: Todo[];
  @Input() options: any;
  @Output() deleteTodo = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  delete(id: number) {
    this.deleteTodo.emit(id);
  }

}
