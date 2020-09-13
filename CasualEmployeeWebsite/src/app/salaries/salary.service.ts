import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EmployeeModel} from '../employees/employee.model';
import {environment} from '../../environments/environment';
import {SalaryModel} from './salary.model';
import {DashboardModel} from '../dashboard/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {

  constructor(private http: HttpClient) { }

  lookup(): Observable<SalaryModel[]> {
    return this.http.get<SalaryModel[]>(`${environment.apiUrl}/report/salaries`);
  }

  get(): Observable<DashboardModel> {
    return this.http.get<DashboardModel>(`${environment.apiUrl}/report/dashboard`);
  }

}
