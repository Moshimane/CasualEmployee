import {BankDetailModel} from './bank-detail.model';
import {RoleModel} from '../roles/role.model';

export class EmployeeModel {
  id: any;
  name: string;
  last_Name: string;
  id_Number: string;
  address: string;
  bank_detail: BankDetailModel;
  role: RoleModel;
  display_Picture: any;
  bank_Name: string;
  bank_Account: string;
  bank_Branch: string;
}
