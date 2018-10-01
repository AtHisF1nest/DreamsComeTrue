import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { TodosService } from '../../_services/todos.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../_models/category';

@Component({
  selector: 'app-dream-list',
  templateUrl: './dream-list.component.html',
  styleUrls: ['./dream-list.component.css']
})
export class DreamListComponent implements OnInit {

  dreamList: Todo[];

  categories: Category[];

  unit = 'zł';

  constructor(private todosService: TodosService, private alertify: AlertifyService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.loadCategories();

    this.route.data.subscribe(data => {
      this.dreamList = data['dreamList'];
      if (!this.dreamList)  {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['']);
      }
    });
  }

  loadCategories() {
    this.todosService.getDreamsCategories().subscribe(categories => {
      this.categories = categories;
    }, error => {
      this.alertify.error(error);
    });
  }

  searchDreams(categories: Category[]) {
    this.todosService.getDreamsByCategories(categories).subscribe(response => {
      if (this.dreamList) {
        this.dreamList = response;
      } else {
        this.alertify.message('Brak wystąpień, spróbuj zmienić kategorie!');
      }
    }, error => {
      this.alertify.error('Wystąpił problem podczas filtrowania danych.');
    });
  }

}
