import { Component, OnInit } from '@angular/core';
import { TodosService } from '../../_services/todos.service';
import { Todo } from '../../_models/todo';
import { AlertifyService } from '../../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Category } from '../../_models/category';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  todoList: Todo[];

  categories: Category[];

  unit = 'h';

  constructor(private todosService: TodosService, private alertify: AlertifyService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.loadCategories();

    this.route.data.subscribe(data => {
      this.todoList = data['todoList'];
      if (!this.todoList)  {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['']);
      }
    });

  }

  loadCategories() {
    this.todosService.getCategories().subscribe(categories => {
      this.categories = categories;
    }, error => {
      this.alertify.error(error);
    });
  }

}
