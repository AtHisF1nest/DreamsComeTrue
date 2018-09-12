import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-todo-list-detail',
  templateUrl: './todo-list-detail.component.html',
  styleUrls: ['./todo-list-detail.component.css']
})
export class TodoListDetailComponent implements OnInit {

  todoItem: Todo;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.todoItem = data['todoItem'];
      if (!this.todoItem)  {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['/nasze-cele']);
      }
    });
  }

}
