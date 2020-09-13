import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {TaskModel} from '../task.model';
import {TaskAssignmentModel} from '../task-assignment.model';
import {EmployeeModel} from '../../employees/employee.model';
import {BankDetailModel} from '../../employees/bank-detail.model';
import {RoleModel} from '../../roles/role.model';
import {TasksService} from '../tasks.service';
import {EmployeeService} from '../../employees/employee.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {

  private action: string;
  private assignees: any[] = [];
  private statuses = ['ToDo', 'In Progress', 'Completed'];
  private task: TaskModel;
  private date: any;
  private employees: EmployeeModel[];
  private assignment: TaskAssignmentModel[];

  constructor(private route: ActivatedRoute,
              private router: Router,
              private taskService: TasksService,
              private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.task = new TaskModel();

    const Id = this.route.snapshot.paramMap.get('id');

    // get roles for the dropdown
    this.employeeService.lookup().subscribe( (data: EmployeeModel[]) => {
      this.employees = data;
      if (Id !== '') {
        // Call get selected task
        this.taskService.get(Id).subscribe( (taskData: TaskModel) => {
          this.task = taskData;
           this.date = formatDate(new Date(this.task.date), 'dd/MM/yyyy', 'en');

          this.assignment = this.task.task_Assignments;
        }, error => {
          console.log(error);
        });
        this.action = 'Edit';
      } else {
        this.action = 'Add New';
        this.assignment = [];
      }
    }, error => {
      console.log(error);
    });
  }

  onSelected(employeeId: any, Id: any ) {
    this.assignment.filter(x => x.id === Id)[0].assigneeId = employeeId;
  }

  onAssign() {
    const taskAssig = new TaskAssignmentModel();
    taskAssig.taskId = this.task.id;
    taskAssig.date = this.task.date;
    this.assignment.push(taskAssig);
  }

  unAssign(Id: any){
    if(this.action === 'Edit' && Id != undefined){
      this.taskService.unassign(Id).subscribe( (res: any) => {
        this.taskService.get(this.task.id).subscribe( (taskData: TaskModel) => {
          this.task = taskData;
          this.date = formatDate(new Date(this.task.date), 'dd/MM/yyyy', 'en');

          this.assignment = this.task.task_Assignments;
        }, error => {
          console.log(error);
        });
      }, error => {
        console.log(error);
      });
    }
    else {
      this.assignment = this.assignment.filter(x => x.id !== Id);
    }
  }

  save() {
    this.task.task_Assignments = this.assignment;
    this.assignment = this.assignment.filter(x => x.assigneeId !== null);
    this.taskService.post(this.task).subscribe( (data: RoleModel) => {
      this.router.navigate(['/tasks']);
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.router.navigate(['/tasks']);
  }

}
