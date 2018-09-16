import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Todo } from '../../_models/todo';
import { AlertifyService } from '../../_services/alertify.service';
import { ManagementService } from '../../_services/management.service';
import { TodosService } from '../../_services/todos.service';

@Component({
  selector: 'app-management-todos',
  templateUrl: './management-todos.component.html',
  styleUrls: ['./management-todos.component.css']
})
export class ManagementTodosComponent implements OnInit {

  todoList: Todo[];

  todoModel: any = {};

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
    private router: Router, private managementService: ManagementService, private todosService: TodosService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.todoList = data['todoList'];
      if (!this.todoList) {
        this.alertify.error('Wystąpił problem podczas pobierania danych.');
        this.router.navigate(['/zarzadzanie']);
      }
    });
  }

  addTodo() {
    this.managementService.addTodo(this.todoModel).subscribe(() => {
      this.alertify.success('Cel został dodany!');
      this.todoModel = {};
      this.loadItems();
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteTodo(id) {
    this.managementService.deleteTodo(id).subscribe(() => {
      this.alertify.success('Cel został usunięty.');
      this.loadItems();
    }, error => {
      this.alertify.error(error);
    });
  }

  loadItems() {
    this.todosService.getItems().subscribe(data => {
      this.todoList = data;
    }, error => {
      this.alertify.error(error);
    });
  }

}
