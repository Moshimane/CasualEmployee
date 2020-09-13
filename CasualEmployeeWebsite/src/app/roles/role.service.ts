import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RoleModel} from './role.model';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  lookup(): Observable<RoleModel[]> {
    return this.http.get<RoleModel[]>(`${environment.apiUrl}/role/lookup`);
  }

  get(Id: any): Observable<RoleModel> {
    return this.http.get<RoleModel>(`${environment.apiUrl}/role?id=${Id}`);
  }

  post(payload: RoleModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/role`, payload);
  }

  delete(Id: any): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/role?id=${Id}`);
  }
}
