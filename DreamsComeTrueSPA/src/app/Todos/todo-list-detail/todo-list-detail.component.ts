import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { HistoryDto } from '../../_models/historyDto';
import { ManagementService } from '../../_services/management.service';
import { TodosService } from '../../_services/todos.service';

@Component({
  selector: 'app-todo-list-detail',
  templateUrl: './todo-list-detail.component.html',
  styleUrls: ['./todo-list-detail.component.css']
})
export class TodoListDetailComponent implements OnInit {

  todoItem: Todo;
  historyOfTodoItem: HistoryDto[];
  doneDate: any;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private router: Router,
    private managementService: ManagementService, private todosService: TodosService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.todoItem = data['todoItem'];
      this.historyOfTodoItem = data['historyOfTodoItem'];
      if (!this.todoItem || !this.historyOfTodoItem)  {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['/nasze-cele']);
      }
    });
  }

  realizeTodo() {
    this.todoItem.lastDone = this.doneDate;
    this.managementService.realizeTodo(this.todoItem).subscribe(() => {
      this.todoItem.lastDone = '';
      this.doneDate = '';
      this.alertify.success('Udało się! Gratulujemy!');
      this.todosService.getHistory(this.todoItem.id).subscribe(response => {
        this.historyOfTodoItem = response;
      }, error => {
        this.alertify.error('Wystąpił problem podczas ładowania danych.');
        this.router.navigate(['/nasze-cele']);
      });
    }, error => {
      this.alertify.error(error);
    });
  }

}
