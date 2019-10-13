import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { PhotoService } from '../_services/photo.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  model: User = {};
  file: File;

  constructor(public authService: AuthService, private userService: UserService, private alertifyService: AlertifyService,
    private router: Router, private photoService: PhotoService) { }

  ngOnInit() {
  }

  holdAvatar(files: FileList) {
    this.file = files.item(0);
  }

  editAvatar() {
    if (this.file) {
      this.photoService.editAvatar(this.file).subscribe(res => {
        this.alertifyService.success('Pomyślnie zaktualizowano avatar');
      }, error => {
        this.alertifyService.error('Nie udało się zaktualizować zdjęcia');
      });
    } else {
      this.alertifyService.error('Brak załadowanego zdjęcia');
    }
  }

  editUserName() {
    if  (this.model.name) {
      this.userService.editUser(this.model).subscribe(res => {
        this.alertifyService.success('Pomyślna edycja.');
        window.location.reload();
      }, error => {
        this.alertifyService.error(error);
      });
    } else {
      this.alertifyService.error('Brak uzupełnionej nazwy');
    }
  }

}
