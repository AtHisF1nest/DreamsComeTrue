import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ManagementType } from '../_models/managementType';
import { ManagementService } from '../_services/management.service';

@Injectable()
export class ManagementResolver implements Resolve<ManagementType[]> {
    constructor(private managementService: ManagementService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<ManagementType[]> {
        return this.managementService.getManagementTypes().pipe(
            catchError(error => {
                this.alertify.error('Wystąpił problem przy pobieraniu danych.');
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
