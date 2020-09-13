import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {TaskModel} from './task.model';
import {TasksService} from './tasks.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  private tasks: TaskModel[];
  constructor(private router: Router, private taskService: TasksService) { }

  ngOnInit(): void {
    this.taskService.lookup().subscribe( (data: Array<TaskModel>) => {
      this.tasks = data;
    }, error => {
      console.log(error);
    });
  }

  formatMyDate(date: Date) {
    return formatDate(new Date(date), 'dd/MM/yyyy', 'en');
  }

  addTask(Id: any) {
    this.router.navigate(['/edit-task', {id: Id}]);
  }

  deleteTask(Id: any) {
    this.taskService.delete(Id).subscribe( (res: any) => {
      this.taskService.lookup().subscribe( (data: Array<TaskModel>) => {
        this.tasks = data;
      }, error => {
        console.log(error);
      });
    }, error => {
      console.log(error);
    });
  }
}
