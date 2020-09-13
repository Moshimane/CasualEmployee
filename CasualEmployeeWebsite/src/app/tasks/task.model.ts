import DateTimeFormat = Intl.DateTimeFormat;
import {TaskAssignmentModel} from './task-assignment.model';

export class TaskModel {
  id: any;
  name: string;
  description: string;
  duration: number;
  date: Date;
  task_Assignments: TaskAssignmentModel[];
  assigneeNames: string[];
  status: string;
}
