import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Todo } from '../_models/todo';
import { TodosService } from '../_services/todos.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class TodoListDetailResolver implements Resolve<Todo> {
    constructor(private todosService: TodosService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Todo> {
        return this.todosService.getItem(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Wystąpił problem przy pobieraniu danych.');
                this.router.navigate(['/nasze-cele']);
                return of(null);
            })
        );
    }
}
