import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { UserService } from './user.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  baseUrl = environment.apiUrl + 'photos/';

constructor(private http: HttpClient) { }

editAvatar(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('photo', file, file.name);
    return this.http.post(this.baseUrl, formData);
}

}
