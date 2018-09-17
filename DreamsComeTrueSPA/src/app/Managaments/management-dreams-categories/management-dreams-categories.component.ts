import { Component, OnInit } from '@angular/core';
import { Category } from '../../_models/category';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { ManagementService } from '../../_services/management.service';
import { TodosService } from '../../_services/todos.service';

@Component({
  selector: 'app-management-dreams-categories',
  templateUrl: './management-dreams-categories.component.html',
  styleUrls: ['./management-dreams-categories.component.css']
})
export class ManagementDreamsCategoriesComponent implements OnInit {

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
    this.managementService.addDreamCategory(this.categoryModel).subscribe(() => {
      this.alertify.success('Udało się dodać kategorię!');
      this.loadData();
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteCategory(id) {
    this.managementService.deleteDreamCategory(id).subscribe(() => {
      this.alertify.success('Udało się usunąć kategorię!');
      this.loadData();
    }, error => {
      this.alertify.error(error);
    });
  }

  loadData() {
    this.todosService.getDreamsCategories().subscribe(data => {
      this.categoryList = data;
      this.categoryModel = {};
    }, error => {
      this.alertify.error('Wystąpił problem podczas pobierania danych.');
    });
  }

}
