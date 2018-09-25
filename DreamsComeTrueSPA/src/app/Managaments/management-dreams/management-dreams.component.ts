import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { ManagementService } from '../../_services/management.service';
import { TodosService } from '../../_services/todos.service';

@Component({
  selector: 'app-management-dreams',
  templateUrl: './management-dreams.component.html',
  styleUrls: ['./management-dreams.component.css']
})
export class ManagementDreamsComponent implements OnInit {

  dreamList: Todo[];

  dreamModel: any = {};

  options: any = {
    isDream: true,
    cost: 'Koszt'
  };

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
    private router: Router, private managementService: ManagementService, private todosService: TodosService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.dreamList = data['dreamList'];
      if (!this.dreamList) {
        this.alertify.error('Wystąpił problem podczas pobierania danych.');
        this.router.navigate(['/zarzadzanie']);
      }
    });
  }

  addDream() {
    this.managementService.addDream(this.dreamModel).subscribe(() => {
      this.alertify.success('Marzenie zostało dodane!');
      this.dreamModel = {};
      this.loadDreams();
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteDream(id) {
    this.managementService.deleteDream(id).subscribe(() => {
      this.alertify.success('Marzenie zostało usunięte!');
      this.loadDreams();
    }, error => {
      this.alertify.error(error);
    });
  }

  loadDreams() {
    this.todosService.getDreams().subscribe(data => {
      this.dreamList = data;
    }, error => {
      this.alertify.error(error);
    });
  }
}
