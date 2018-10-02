import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-find-pair',
  templateUrl: './find-pair.component.html',
  styleUrls: ['./find-pair.component.css']
})
export class FindPairComponent implements OnInit {

  userList: User[];
  userModel: any = { name: '' };

  constructor(private route: ActivatedRoute, private router: Router, private alertify: AlertifyService,
    private userService: UserService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.userList = data['userList'];
      if (!this.userList) {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['']);
      }
    });
  }

  search() {
    this.userService.getUsers(this.userModel.name).subscribe(response => {
      if (response) {
        this.userList = response;
        if (this.userList.length === 0) {
          this.alertify.message('Nic nie znaleziono');
        }
      } else {
        this.userList = [];
        this.alertify.message('Nic nie znaleziono');
      }
    }, error => {
      this.alertify.message('Nic nie znaleziono');
    });
  }

  inviteUser(user: User) {
    this.userService.inviteUser(user).subscribe(() => {
      this.alertify.message('Zaproszono.');
    }, error => {
      this.alertify.error('Nie udało się zaprosić.');
    });
  }

  unInviteUser(user: User) {

  }

}
