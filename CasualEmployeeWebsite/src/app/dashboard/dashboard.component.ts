import { Component, OnInit } from '@angular/core';
import * as Chartist from 'chartist';
import {Router} from '@angular/router';
import {SalaryService} from '../salaries/salary.service';
import {RoleModel} from '../roles/role.model';
import {DashboardModel} from './dashboard.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  private dashboard: DashboardModel;
  constructor(private router: Router, private salariesService: SalaryService) { }

  ngOnInit() {
    this.salariesService.get().subscribe( (data: DashboardModel) => {
      this.dashboard = data;
    }, error => {
      console.log(error);
    });
    //this.chartConfig();
  }
  viewEmployees() {
    this.router.navigate(['/salaries']);
  }

  viewTasks() {
    this.router.navigate(['/tasks']);
  }
}
