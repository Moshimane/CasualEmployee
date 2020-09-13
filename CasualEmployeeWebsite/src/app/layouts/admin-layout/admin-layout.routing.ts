import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { EmployeesComponent } from '../../employees/employees.component';
import {TasksComponent} from '../../tasks/tasks.component';
import {EditTaskComponent} from '../../tasks/edit-task/edit-task.component';
import {SalariesComponent} from '../../salaries/salaries.component';
import {RolesComponent} from '../../roles/roles.component';
import {EditRoleComponent} from '../../roles/edit-role/edit-role.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'employees',     component: EmployeesComponent },
    { path: 'tasks', component: TasksComponent},
    { path: 'edit-task', component: EditTaskComponent},
    { path: 'salaries', component: SalariesComponent},
    { path: 'roles', component: RolesComponent},
    { path: 'edit-role', component: EditRoleComponent}
];
