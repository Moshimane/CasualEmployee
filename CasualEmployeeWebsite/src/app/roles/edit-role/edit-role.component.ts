import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {RoleService} from '../role.service';
import {RoleModel} from '../role.model';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css']
})
export class EditRoleComponent implements OnInit {
  private action: string;
  private role: RoleModel;

  constructor(private route: ActivatedRoute,
              private router: Router ,
              private roleService: RoleService) { }

  ngOnInit() {
    this.role = new RoleModel();
    const Id = this.route.snapshot.paramMap.get('id');
    if (Id !== '') {
      // Call get Employee by Id service
      this.roleService.get(Id).subscribe( (data: RoleModel) => {
        console.log(data);
        this.role = data;
      }, error => {
        console.log(error);
      });
      this.action = 'Edit';
    } else {
      this.action = 'Add New';
    }
  }

  save() {
    //Save entry
    this.roleService.post(this.role).subscribe( (data: RoleModel) => {
      this.router.navigate(['/roles']);
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.router.navigate(['/roles']);
  }

}
