using CasualEmployee.Api.Controllers.Employee;
using CasualEmployee.Api.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetLookups(CasualEmployeeContext db);

        Employee GetById(Guid Id, CasualEmployeeContext db);

        Guid Upsert(EmployeeDTO employee, CasualEmployeeContext db);

        Task<int> getPhotoAsync(IFormCollection file, CasualEmployeeContext db);
    }
}
