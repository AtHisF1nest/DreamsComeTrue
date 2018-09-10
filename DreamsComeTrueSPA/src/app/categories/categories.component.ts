import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { TodosService } from '../_services/todos.service';
import { Category } from '../_models/category';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  baseUrl = environment.apiUrl + 'todos/';

  categories: Category[];

  constructor(private todosService: TodosService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.todosService.getCategories().subscribe(categories => {
      this.categories = categories;
      console.log(categories);
    }, error => {
      this.alertify.error(error);
    });
  }

}
