import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { Category } from '../../_models/category';
import { ManagementService } from '../../_services/management.service';
import { TodosService } from '../../_services/todos.service';

@Component({
  selector: 'app-management-categories',
  templateUrl: './management-categories.component.html',
  styleUrls: ['./management-categories.component.css']
})
export class ManagementCategoriesComponent implements OnInit {

  categoryList: Category[];

  categoryModel: any = {};

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private router: Router,
    private managementService: ManagementService, private todosService: TodosService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.categoryList = data['categoryList'];
    }, error => {
      this.alertify.error('Wystąpił problem podczas pobierania danych.');
      this.router.navigate(['/zarzadzanie']);
    });
  }

  addCategory() {
    this.managementService.addCategory(this.categoryModel).subscribe(() => {
      this.alertify.success('Udało się dodać kategorię!');
      this.todosService.getCategories().subscribe(data => {
        this.categoryList = data;
        this.categoryModel = {};
      }, error => {
        this.alertify.error('Wystąpił problem podczas pobierania danych.');
      });
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteCategory(id) {
    this.managementService.deleteCategory(id).subscribe(() => {
      this.alertify.success('Udało się usunąć kategorię!');
      this.todosService.getCategories().subscribe(data => {
        this.categoryList = data;
      }, error => {
        this.alertify.error('Wystąpił problem podczas pobierania danych.');
      });
    }, error => {
      this.alertify.error(error);
    });
  }

}
