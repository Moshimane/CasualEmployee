using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualEmployee.Api.Models;
using CasualEmployee.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasualEmployee.Api.Controllers.Role
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly ILogger<RoleController> _logger;
        private readonly IRoleRepository roleRepository;
        private readonly CasualEmployeeContext db;

        public RoleController(ILogger<RoleController> logger, IRoleRepository roleRepository, CasualEmployeeContext db)
        {
            _logger = logger;
            this.roleRepository = roleRepository;
            this.db = db;
        }

        [HttpGet]
        [Route("lookup")]
        public IEnumerable<RoleDTO> Get()
        {
            var roleData = this.roleRepository.GetLookups(this.db);
            List<RoleDTO> roles = new List<RoleDTO>();
            foreach(var role in roleData)
            {
                roles.Add(new RoleDTO { Id = role.Id, Name = role.Name, Rate = role.Rate });
            }
            return roles;
        }

        [HttpGet]
        [Route("")]
        public RoleDTO Get([FromQuery] Guid Id)
        {
            RoleDTO role = new RoleDTO();
            var roleData = this.roleRepository.GetById(Id, this.db);
            if(roleData != null)
            {
                role.Id = roleData.Id;
                role.Name = roleData.Name;
                role.Rate = roleData.Rate;
            }
            return role;
        }

        [HttpPost]
        [Route("")]
        public RoleDTO Post([FromBody] RoleDTO dto)
        {
            this.roleRepository.Upsert(dto, this.db);
            RoleDTO role = new RoleDTO();
            return role;
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromQuery] Guid Id)
        {
            var role = this.db.Roles.FirstOrDefault(x => x.Id == Id);
            var employees = this.db.Employees.Where(x => x.Role_Id == Id);
            foreach (var employee in employees)
            {
                //Delete from time sheet
                var times = this.db.Employee_Time_Sheets.Where(x => x.AssigneeId == employee.Id);
                if (times != null)
                {
                    this.db.Employee_Time_Sheets.RemoveRange(times);
                }
                var tasks = this.db.Task_Assignments.Where(x => x.AssigneeId == employee.Id);
                if (tasks != null)
                {
                    this.db.Task_Assignments.RemoveRange(tasks);
                }
                this.db.Employees.Remove(employee);
            }
            this.db.Roles.Remove(role);
            this.db.SaveChanges();

            return this.Ok();
        }
    }
}
