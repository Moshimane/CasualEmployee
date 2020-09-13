using CasualEmployee.Api.Controllers.Role;
using CasualEmployee.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public interface IRoleRepository
    {

        IList<Role> GetLookups(CasualEmployeeContext db);

        Role GetById(Guid Id, CasualEmployeeContext db);

        void Upsert(RoleDTO role, CasualEmployeeContext db);
    }
}
