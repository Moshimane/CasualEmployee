using CasualEmployee.Api.Controllers.Employee;
using CasualEmployee.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        public IList<Employee> GetLookups(CasualEmployeeContext db)
        {
            var result = new List<Employee>();
            var employees = db.Employees.ToList();
            foreach(var employee in employees)
            {
                employee.Role = db.Roles.FirstOrDefault(x => x.Id == employee.Role_Id);
                employee.Bank_Detail = db.Bank_Details.FirstOrDefault(x => x.Id == employee.Bank_Detail_Id);
                result.Add(employee);
            }

            return result;
        }

        public Employee GetById(Guid Id, CasualEmployeeContext db)
        {
           var employee = db.Employees.FirstOrDefault(x => x.Id == Id);
            employee.Role = db.Roles.FirstOrDefault(x => x.Id == employee.Role_Id);
            employee.Bank_Detail = db.Bank_Details.FirstOrDefault(x => x.Id == employee.Bank_Detail_Id);
            return employee;
        }

        public Guid Upsert(EmployeeDTO employee, CasualEmployeeContext db)
        {
            var exists = db.Employees.Any(x => x.Id == employee.Id);
            if (exists)
            {
                var entity = db.Employees.FirstOrDefault(x => x.Id == employee.Id);
                entity.Name = employee.Name;
                entity.Id_Number = employee.Id_Number;
                entity.Last_Name = employee.Last_Name;
                entity.Address = employee.Address;
                entity.Bank_Detail_Id = employee.Bank_Detail.Id;
                entity.Role_Id = employee.Role.Id;
                entity.Bank_Detail = new Bank_Detail();
                entity.Bank_Detail.Id = entity.Bank_Detail_Id;
                entity.Bank_Detail.Name = employee.Bank_Detail.Name;
                entity.Bank_Detail.Account_Number = employee.Bank_Detail.Account_Number;
                entity.Bank_Detail.Branch_Number = employee.Bank_Detail.Branch_Number;
                db.Employees.Update(entity);
                db.Bank_Details.Update(entity.Bank_Detail);
                db.SaveChanges();

                return entity.Id;
            }
            else
            {
                var entity = new Employee();
                entity.Id = Guid.NewGuid();
                entity.Name = employee.Name;
                entity.Id_Number = employee.Id_Number;
                entity.Last_Name = employee.Last_Name;
                entity.Address = employee.Address;
                entity.Bank_Detail_Id = Guid.NewGuid();
                entity.Bank_Detail = new Bank_Detail();
                entity.Bank_Detail.Id = entity.Bank_Detail_Id;
                entity.Bank_Detail.Name = employee.Bank_Detail.Name;
                entity.Bank_Detail.Account_Number = employee.Bank_Detail.Account_Number;
                entity.Bank_Detail.Branch_Number = employee.Bank_Detail.Branch_Number;
                entity.Role_Id = employee.Role.Id;
                db.Employees.Add(entity);

                db.Bank_Details.Add(entity.Bank_Detail);
                db.SaveChanges();
                return entity.Id;
            }
        }

        public async Task<int> getPhotoAsync(IFormCollection file, CasualEmployeeContext db)
        {
            try 
            { 
                var File = file.Files[0];
                Guid Id = Guid.Parse(file["Id"].ToString());
                var employee = db.Employees.FirstOrDefault(x => x.Id == Id);
                await using (var ms = new MemoryStream())
                {
                    await File.CopyToAsync(ms);
                     //employee.Display_Picture = ms.ToArray();
                    Employee entity = new Employee();
                    entity = employee;
                    entity.Display_Picture = ms.ToArray();
                    db.Employees.Update(entity);
                     db.SaveChanges();
                }
                return -1;
            }
            catch(Exception e)
            {
                throw;
            }
           
        }
    }
}
