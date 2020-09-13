import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EmployeeModel} from '../employees/employee.model';
import {environment} from '../../environments/environment';
import {TaskModel} from './task.model';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  constructor(private http: HttpClient) { }

  lookup(): Observable<TaskModel[]> {
    return this.http.get<TaskModel[]>(`${environment.apiUrl}/task/lookup`);
  }

  get(Id: any): Observable<TaskModel> {
    return this.http.get<TaskModel>(`${environment.apiUrl}/task?id=${Id}`);
  }

  post(payload: TaskModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/task`, payload);
  }

  delete(Id: any): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/task?id=${Id}`);
  }

  unassign(Id: any): Observable<any> {
    return this.http.get(`${environment.apiUrl}/task/unassign?id=${Id}`);
  }
}
