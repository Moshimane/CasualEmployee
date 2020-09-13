import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {RoleModel} from './role.model';
import {RoleService} from './role.service';
import {TaskModel} from '../tasks/task.model';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  private roles: RoleModel[];
  constructor(private router: Router, private roleService: RoleService) { }

  ngOnInit(): void {
    this.roleService.lookup().subscribe( (data: Array<RoleModel>) => {
      console.log(data);
      this.roles = data;
    }, error => {
      console.log(error);
    });
  }

  addRole() {
    this.router.navigate(['/edit-role', {id: ''}]);
  }

  editRole(id: any) {
    this.router.navigate(['/edit-role', {id: id}]);
  }

  deleteRole(Id: any){
    this.roleService.delete(Id).subscribe( (res: any) => {
      this.roleService.lookup().subscribe( (data: Array<RoleModel>) => {
        this.roles = data;
      }, error => {
        console.log(error);
      });
    }, error => {
      console.log(error);
    });
  }
}
