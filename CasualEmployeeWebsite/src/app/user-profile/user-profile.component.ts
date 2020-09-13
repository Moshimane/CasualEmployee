import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {RoleModel} from '../roles/role.model';
import {EmployeeModel} from '../employees/employee.model';
import {EmployeeService} from '../employees/employee.service';
import {BankDetailModel} from '../employees/bank-detail.model';
import {RoleService} from '../roles/role.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  private action: string;
  private employee: EmployeeModel;
  private roles: RoleModel[];
  private url: any;
  private roleDisplay: string;
  private fileUploaded: File;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private employeeService: EmployeeService,
              private roleService: RoleService) { }

  ngOnInit() {
      // Call get Employee by Id service
      this.employee = new EmployeeModel();
      this.employee.bank_detail = new BankDetailModel();
      this.employee.role = new RoleModel();
      const Id = this.route.snapshot.paramMap.get('id');

      // get roles for the dropdown
      this.roleService.lookup().subscribe( (data: RoleModel[]) => {
        this.roles = data;
        if (Id !== '') {
          // Call get selected employee
          this.employeeService.get(Id).subscribe( (empData: EmployeeModel) => {
            this.employee = empData;
            //this.displayRole();
          }, error => {
            console.log(error);
          });
          this.action = 'Edit';
        } else {
          this.action = 'Add New';
          //this.roleDisplay = 'Select Role';
        }
      }, error => {
        console.log(error);
      });
  }

  loadFile(event) {
    this.fileUploaded = event.target.files[0];
    const reader = new FileReader();
    reader.onload = (eve: any) => {
      this.url = eve.target.result;
      console.log(this.url);
    };
    reader.readAsDataURL(event.target.files[0]);
  }


  updateFile(event) {
    this.fileUploaded = event.target.files[0];
    console.log(this.fileUploaded);
  }

  download() {
    /*const formData = new FormData();
    formData.set('JobHeaderId', this.jobHeaderId);
    formData.set('PartRequisationId', this.params.data.id);
    this.service.downloadFile(formData).subscribe(x => {
      const shortDate = formatDate(new Date(), 'yyyy/MM/dd', 'en');
      const fileName = 'ProBlastParQuotation' + '-' + shortDate + '.xlsx';
      const blob = new Blob([x], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
      saveAs(blob, fileName);
    }, (err) => {
      alert('Part has no Quote!');
    });*/
  }

  /*displayRole(){
    this.roleDisplay = this.roles.filter(x => x.id === this.employee.role.id)[0].name;
  }*/

  uploadDP() {

  }

  onNameChange(){

  }

  onBranchChange() {
    this.employee.bank_detail.branch_Number = this.employee.bank_Branch;
  }

  onAccChange(){
    this.employee.bank_detail.account_Number = this.employee.bank_Account;
  }

  onBankChange(){
    this.employee.bank_detail.name = this.employee.bank_Name;
  }

  save() {

    const formData = new FormData();
    formData.set('File', this.fileUploaded);

    this.employeeService.post(this.employee).subscribe( (data: any) => {
      console.log(data);
      formData.set('Id', data);
      this.employeeService.upload(formData).subscribe( (res: any) => {
        this.router.navigate(['/employees']);
      }, error => {
        console.log(error);
      });
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.router.navigate(['/employees']);
  }

}
