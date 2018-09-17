import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TodosService } from '../_services/todos.service';
import { Todo } from '../_models/todo';

@Injectable()
export class ManagementDreamsResolver implements Resolve<Todo[]> {
    constructor(private todosService: TodosService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Todo[]> {
        return this.todosService.getDreams().pipe(
            catchError(error => {
                this.alertify.error('Wystąpił problem przy pobieraniu danych.');
                this.router.navigate(['/zarzadzanie']);
                return of(null);
            })
        );
    }
}
