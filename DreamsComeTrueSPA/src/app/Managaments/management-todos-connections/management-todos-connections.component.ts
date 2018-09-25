import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { Category } from '../../_models/category';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { TodosService } from '../../_services/todos.service';
import { ManagementService } from '../../_services/management.service';
import { TodoConnection } from '../../_models/todoConnection';
import { isDefaultChangeDetectionStrategy } from '@angular/core/src/change_detection/constants';

@Component({
  selector: 'app-management-todos-connections',
  templateUrl: './management-todos-connections.component.html',
  styleUrls: ['./management-todos-connections.component.css']
})
export class ManagementTodosConnectionsComponent implements OnInit {

  todoList: Todo[];
  connectedTodoList: Todo[];

  categoryList: Category[];

  selectedCategory: Category;
  selectedTodo: Todo;
  selectedConnection: Todo;

  postConnection: TodoConnection;

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private todosService: TodosService,
    private managementService: ManagementService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.categoryList = data['categoryList'];
      if (this.categoryList) {
        this.categoryList[0].active = true;
        this.selectedCategory = this.categoryList[0];
        this.loadConnections();
        this.loadNotConnected();
      }
    });
  }

  setActiveTodo(item: Todo) {
    this.todoList.forEach(x => x.active = false);
    item.active = true;
    this.selectedTodo = item;
  }

  setActiveTodoConnected(item: Todo) {
    this.connectedTodoList.forEach(x => x.active = false);
    item.active = true;
    this.selectedConnection = item;
  }

  setActiveCategory(item: Category) {
    this.categoryList.forEach(x => x.active = false);
    item.active = true;
    this.selectedCategory = item;
    this.loadConnections();
    this.loadNotConnected();
  }

  loadConnections() {
    this.todosService.getTodosConnections(this.selectedCategory.id).subscribe(data => {
      this.connectedTodoList = data;
    }, error => {
      this.alertify.error('Wystąpił problem podczas pobierania danych.');
    });
  }

  loadNotConnected() {
    this.todosService.getNotConnectedItems(this.selectedCategory.id).subscribe(data => {
      this.todoList = data;
    }, error => {
      this.alertify.error('Wystąpił problem podczas pobierania danych.');
    });
  }

  connect() {
    if  (!this.selectedCategory || !this.selectedTodo)  {
      this.alertify.message('Zaznacz najpierw cel który chcesz dołączyć do kategorii.');
      return;
    }

    this.postConnection = {
      categoryId: this.selectedCategory.id,
      itemId: this.selectedTodo.id
    };

    this.managementService.connectItems(this.postConnection).subscribe(() => {
      this.alertify.success('Połączono.');

      this.selectedCategory.countOfItems++;
      this.selectedTodo.active = false;
      this.connectedTodoList.push(this.selectedTodo);
      const indexOf = this.todoList.indexOf(this.selectedTodo);
      this.todoList.splice(indexOf, 1);

    }, error => {
      this.alertify.error('Połączenie nie powiodło się.');
    });
  }

  unConnect() {
    if  (!this.selectedCategory || !this.selectedConnection)  {
      this.alertify.message('Zaznacz cel który chcesz odłączyć.');
      return;
    }

    this.postConnection = {
      categoryId: this.selectedCategory.id,
      itemId: this.selectedConnection.id
    };

    this.managementService.unConnectItems(this.postConnection).subscribe(() => {
      this.alertify.message('Odłączono.');

      this.selectedCategory.countOfItems--;
      this.selectedConnection.active = false;
      this.todoList.push(this.selectedConnection);
      const indexOf = this.connectedTodoList.indexOf(this.selectedConnection);
      this.connectedTodoList.splice(indexOf, 1);

    }, error => {
      this.alertify.error('Odłączenie nie powiodło się.');
    });
  }
}
