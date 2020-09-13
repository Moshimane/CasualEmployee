using CasualEmployee.Api.Controllers.Role;
using CasualEmployee.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        public IList<Role> GetLookups(CasualEmployeeContext db)
        {
            return db.Roles.ToList();
        }

        public Role GetById(Guid Id, CasualEmployeeContext db)
        {
            return db.Roles.FirstOrDefault(x => x.Id == Id);
        }

        public void Upsert(RoleDTO role, CasualEmployeeContext db)
        {
            var exists = db.Roles.Any(x => x.Id == role.Id);
            if (exists)
            {
                var entity = db.Roles.FirstOrDefault(x => x.Id == role.Id);
                entity.Name = role.Name;
                entity.Rate = role.Rate;
                db.Roles.Update(entity);
                db.SaveChanges();
            }
            else
            {
                var entity = new Role();
                entity.Id = Guid.NewGuid();
                entity.Name = role.Name;
                entity.Rate = role.Rate;
                db.Roles.Add(entity);
                db.SaveChanges();
            }
        }
    }
}
