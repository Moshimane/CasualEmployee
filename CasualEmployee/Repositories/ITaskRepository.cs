using CasualEmployee.Api.Controllers.Task;
using CasualEmployee.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public interface ITaskRepository
    {
        IList<Models.Task> GetLookups(CasualEmployeeContext db);

        Models.Task GetById(Guid Id, CasualEmployeeContext db);

        void Upsert(TaskDTO task, CasualEmployeeContext db);
    }
}
