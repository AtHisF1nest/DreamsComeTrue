import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Todo } from '../../_models/todo';
import { ManagementService } from '../../_services/management.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-management-todos-list',
  templateUrl: './management-todos-list.component.html',
  styleUrls: ['./management-todos-list.component.css']
})
export class ManagementTodosListComponent implements OnInit {

  @Input() todoList: Todo[];
  @Input() options: any;
  @Output() deleteTodo = new EventEmitter();

  constructor(private managementService: ManagementService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  delete(id: number) {
    this.deleteTodo.emit(id);
  }

  edit(item: Todo) {
    this.managementService.editItem(item).subscribe(() => {
      this.alertify.success('Edycja przebiegła pomyślnie.');
    }, error => {
      this.alertify.error('Edycja się nie powiodła.');
    })
  }

}
