import {EmployeeModel} from '../employees/employee.model';
import DateTimeFormat = Intl.DateTimeFormat;

export class TaskAssignmentModel {
  id: any;
  taskId: any;
  assigneeId: any;
  assignee: EmployeeModel;
  date: Date;
  hours: number;
}
