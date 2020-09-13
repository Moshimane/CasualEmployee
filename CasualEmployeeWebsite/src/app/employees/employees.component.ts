import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {EmployeeModel} from './employee.model';
import {EmployeeService} from './employee.service';
import {RoleModel} from '../roles/role.model';
import {TaskModel} from '../tasks/task.model';
import {DomSanitizer} from '@angular/platform-browser';

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: '/user-profile', title: 'Employee Profile',  icon:'users_single-02', class: '' }
];

@Component({
  selector: 'app-table-list',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  private employees: EmployeeModel[];
  private imageUrl: any;
  private domSanitizer: DomSanitizer;

  constructor(private router: Router, private employeeService: EmployeeService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.employeeService.lookup().subscribe( (data: Array<EmployeeModel>) => {
      this.employees = data;
      const bArr = data[0].display_Picture;
      let TYPED_ARRAY = new Uint8Array(bArr);
      /*const STRING_CHAR = TYPED_ARRAY.reduce((data, byte)=> {
        return data + String.fromCharCode(byte);
      }, ''));*/
      const STRING_CHAR = String.fromCharCode.apply(null, TYPED_ARRAY);
      let base64String = btoa(STRING_CHAR);
      //this.imageUrl = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64, ' + base64String);
      const blob = new Blob( [ data[0].display_Picture ], { type: "image/jpeg" } );
      const blobUrl = URL.createObjectURL( blob );
      let arr = blobUrl.split('/');
      this.imageUrl = arr[arr.length - 1] + '.jpeg'
    }, error => {
      console.log(error);
    });
  //this.download();
  }

  download() {
    this.employeeService.downloadFiles().subscribe(x => {
      console.log(x);
      /*const shortDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');
      const fileName = 'ProBlastParQuotation' + '-' + shortDate + '.xlsx';
      const blob = new Blob([x], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
      saveAs(blob, fileName);*/
    }, (err) => {
      alert('Part has no Quote!');
    });
  }

  addEmployee(Id: any) {
    this.router.navigate(['/user-profile', {id: Id}]);
  }

  deleteEmployee(Id: any) {
    this.employeeService.delete(Id).subscribe( (res: any) => {
      this.employeeService.lookup().subscribe( (data: Array<EmployeeModel>) => {
        this.employees = data;
      }, error => {
        console.log(error);
      });
    }, error => {
      console.log(error);
    });
  }
}
