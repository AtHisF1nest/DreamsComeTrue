import { Component, OnInit, Input } from '@angular/core';
import { Todo } from '../../_models/todo';

@Component({
  selector: 'app-todo-list-item',
  templateUrl: './todo-list-item.component.html',
  styleUrls: ['./todo-list-item.component.css']
})
export class TodoListItemComponent implements OnInit {
  @Input() item: Todo;


  constructor() { }

  ngOnInit() {
  }

}