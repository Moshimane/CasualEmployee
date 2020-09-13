import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RoleModel} from '../roles/role.model';
import {environment} from '../../environments/environment';
import {EmployeeModel} from './employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  lookup(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(`${environment.apiUrl}/employee/lookup`);
  }

  get(Id: any): Observable<EmployeeModel> {
    return this.http.get<EmployeeModel>(`${environment.apiUrl}/employee?id=${Id}`);
  }

  post(payload: EmployeeModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/employee`, payload);
  }

  upload( formData: FormData): Observable<Blob> {
    return this.http.post(`${environment.apiUrl}/employee/upload`, formData, { responseType: 'blob'});
  }

  downloadFiles(): Observable<Blob> {
    return this.http.post(`${environment.apiUrl}/employee/download`, new FormData(), {responseType: 'blob'});
  }

  delete(Id: any): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/employee?id=${Id}`);
  }
}
