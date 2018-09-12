import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ManagementType } from '../_models/managementType';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManagementService {

  baseUrl = environment.apiUrl + 'management/';

  constructor(private http: HttpClient) { }

  getManagementTypes(): Observable<ManagementType[]> {
    return this.http.get<ManagementType[]>(this.baseUrl);
  }

}
