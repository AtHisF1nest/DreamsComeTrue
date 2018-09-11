import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo-list-detail',
  templateUrl: './todo-list-detail.component.html',
  styleUrls: ['./todo-list-detail.component.css']
})
export class TodoListDetailComponent implements OnInit {

  todoItem: Todo;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.todoItem = data['todoItem'];
    })
  }

}
