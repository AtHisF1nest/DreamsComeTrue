import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  user: User;

  constructor(public authService: AuthService, private alertify: AlertifyService,
    private router: Router, private userService: UserService) { }

  ngOnInit() {
    if (!this.user) {
      this.userService.getCurrentUser().subscribe(res => {
        this.user = res;
      });
    }
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Zalogowano pomyślnie!');
    }, error => {
      this.alertify.error('Logowanie nie powiodło się.');
    }, () => {
      if (!this.user) {
        this.userService.getCurrentUser().subscribe(res => {
          this.user = res;
        });
      }
      this.router.navigate(['/nasze-cele']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.user = null;
    this.alertify.message('Będziemy na Ciebie czekać :)');
    this.router.navigate(['/strona-glowna']);
  }

}
