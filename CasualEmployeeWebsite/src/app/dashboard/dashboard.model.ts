import {SalaryModel} from '../salaries/salary.model';

export class DashboardModel {
  names: string[]
  salaries: number[];
  dailyHours: number[];
  days: string[];
  nameHours: number[];
  totalSalaries: SalaryModel[];
  tasks: any[];
}
