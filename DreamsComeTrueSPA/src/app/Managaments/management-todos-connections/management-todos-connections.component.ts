import { Component, OnInit } from '@angular/core';
import { Todo } from '../../_models/todo';
import { Category } from '../../_models/category';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-management-todos-connections',
  templateUrl: './management-todos-connections.component.html',
  styleUrls: ['./management-todos-connections.component.css']
})
export class ManagementTodosConnectionsComponent implements OnInit {

  todoList: Todo[];

  categoryList: Category[];

  constructor(private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.todoList = data['todoList'];
      this.categoryList = data['categoryList'];
    });
  }

}
