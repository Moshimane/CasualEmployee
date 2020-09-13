import { Component, OnInit } from '@angular/core';
import {RoleModel} from '../roles/role.model';
import {SalaryModel} from './salary.model';
import {SalaryService} from './salary.service';
import {EmployeeModel} from '../employees/employee.model';
import {formatDate} from '@angular/common';
import * as moment from 'moment';

@Component({
  selector: 'app-salaries',
  templateUrl: './salaries.component.html',
  styleUrls: ['./salaries.component.css']
})
export class SalariesComponent implements OnInit {

  private salaries: SalaryModel[];
  private salaryTotals: SalaryModel[];
  private groupedSalaries: SalaryModel[];
  constructor(private salaryService: SalaryService) { }

  ngOnInit(): void {
    this.salaryService.lookup().subscribe( (data: Array<SalaryModel>) => {
      this.salaries = data;
      const result = this.groupBy(data, function(item) {
        return [item.first_Name, item.last_Name];
      });
      const data1 = [];
      result.forEach(function(value){
        const model = new SalaryModel();
        model.first_Name = value[0].first_Name;
        model.last_Name = value[0].last_Name;
        model.salary = 0;
        model.worked_Hours = 0;
        value.forEach(function(personTimes){
         model.salary += personTimes.worked_Hours * personTimes.rate;
          model.worked_Hours += personTimes.worked_Hours;
        });
        console.log(model);
        data1.push(model);
      });
      this.groupedSalaries = data.filter(x => x.last_Name === data1[0].last_Name && x.first_Name === data1[0].first_Name);
      const total = new SalaryModel();
      total.role = 'ToTal';
      total.rate = -1;
      total.worked_Hours = data1[0].worked_Hours;
      total.salary = data1[0].salary;
      this.groupedSalaries.push(total);
      this.salaryTotals = data1;
    }, error => {
      console.log(error);
    });
  }

  groupBy( array , f ) {
    const groups = {};
    array.forEach( function( o ) {
      const group = JSON.stringify( f(o) );
      groups[group] = groups[group] || [];
      groups[group].push( o );
    });
    return Object.keys(groups).map( function( group ) {
      return groups[group];
    });
  }

  formatMyDate(date: Date) {
    console.log(date);
    if(date !== undefined) {
      return formatDate(new Date(date), 'dd/MM/yyyy', 'en');
    }
    else{
      return '';
    }
  }

  view(salary: SalaryModel) {
    this.groupedSalaries = this.salaries.filter(x => x.last_Name === salary.last_Name && x.first_Name === salary.first_Name);
    const total = new SalaryModel();
    total.role = 'ToTal';
    total.rate = -1;
    total.worked_Hours = salary.worked_Hours;
    total.salary = salary.salary;
    this.groupedSalaries.push(total);
  }

}
