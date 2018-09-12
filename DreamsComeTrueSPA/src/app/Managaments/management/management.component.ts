import { Component, OnInit } from '@angular/core';
import { ManagementType } from '../../_models/managementType';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent implements OnInit {

  managementTypes: ManagementType[];

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.managementTypes = data['managementTypes'];
      if (!this.managementTypes)  {
        this.alertify.error('Wystąpił problem przy pobieraniu danych.');
        this.router.navigate(['']);
      }
    });
  }

}
