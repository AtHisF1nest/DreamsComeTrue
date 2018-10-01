import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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

  @Output() search = new EventEmitter();
  @Output() showDone = new EventEmitter();
  @Input() categories: Category[];
  @Input() showHistoryButton: Boolean;
  selectedCategories: Category[];

  constructor(private todosService: TodosService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.selectedCategories = [];
  }

  resetCategories() {
    this.categories.forEach(x => x.active = false);
    this.emitSearch();
  }

  showDoneButton() {
    this.resetCategories();
    this.showDone.emit();
  }

  toggleActive(item: Category) {
    item.active = !item.active;
    this.emitSearch();
  }

  emitSearch() {
    this.selectedCategories = [];
    this.categories.forEach(x => {
      if (x.active) {
        this.selectedCategories.push(x);
      }
    });

    this.search.emit(this.selectedCategories);
  }

}
