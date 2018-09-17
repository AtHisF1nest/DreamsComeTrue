import { Component, OnInit, Input } from '@angular/core';
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

  @Input() categories: Category[];

  constructor(private todosService: TodosService, private alertify: AlertifyService) { }

  ngOnInit() {

  }

  anyCategoryActive() {
    if (!this.categories) {
      return false;
    }
    this.categories.forEach(element => {
      if (element.active) {
        return true;
      }
    });

    return false;
  }

}
