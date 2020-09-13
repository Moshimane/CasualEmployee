import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { TasksComponent } from './tasks/tasks.component';
import { EditTaskComponent } from './tasks/edit-task/edit-task.component';
import { SalariesComponent } from './salaries/salaries.component';
import { RolesComponent } from './roles/roles.component';
import { EditRoleComponent } from './roles/edit-role/edit-role.component';
import {RoleService} from './roles/role.service';
import {EmployeeService} from './employees/employee.service';
import {TasksService} from './tasks/tasks.service';
import {SalaryService} from './salaries/salary.service';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    NgbModule,
    ToastrModule.forRoot()
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    TasksComponent,
    EditTaskComponent,
    SalariesComponent,
    RolesComponent,
    EditRoleComponent

  ],
  providers: [RoleService, EmployeeService, TasksService, SalaryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
