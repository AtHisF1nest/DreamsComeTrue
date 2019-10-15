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
  invitations: User[];
  userModel: any = { name: '' };

  constructor(private route: ActivatedRoute, private router: Router, private alertify: AlertifyService,
    private userService: UserService) { }

  ngOnInit() {
    this.userService.getCurrentUser().subscribe(res => {
      if (res.hasPair) {
        this.router.navigate(['/nasze-cele']);
        this.alertify.message('Masz już swoją parę!');
      }
    });
    this.route.data.subscribe(data => {
      this.userList = data['userList'];
      if (this.userList.length === 0) {
        this.alertify.message('Masz już swoją parę!');
        this.router.navigate(['/nasze-cele']);
      }
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
      user.isInvited = true;
    }, error => {
      this.alertify.error('Nie udało się zaprosić');
    });
  }

  acceptInvite(userId) {
    this.userService.acceptInvite(userId).subscribe(res => {
      this.alertify.success('Pomyślnie przyjęto zaproszenie, jesteście parą!');
      this.router.navigate(['/nasze-cele']);
    }, error => {
      this.alertify.error('Coś poszło nie tak');
    });
  }

  unInviteUser(user: User) {
    this.userService.unInviteUser(user).subscribe(() => {
      this.alertify.success('Pomyślnie usunięto zaproszenie');
      user.isInvited = false;
    }, error => {
      this.alertify.error('Nie udało się wykonać akcji');
    });
  }

}
