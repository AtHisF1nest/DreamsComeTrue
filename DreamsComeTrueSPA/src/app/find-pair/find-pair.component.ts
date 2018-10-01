import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-find-pair',
  templateUrl: './find-pair.component.html',
  styleUrls: ['./find-pair.component.css']
})
export class FindPairComponent implements OnInit {

  userList: User[];

  constructor(private route: ActivatedRoute, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.userList = data['userList'];
      if (!this.userList) {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['']);
      }
    });
  }

}
