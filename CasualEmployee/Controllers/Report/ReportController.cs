using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualEmployee.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasualEmployee.Api.Controllers.Report
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    { 
        private readonly ILogger<ReportController> _logger;
        private readonly CasualEmployeeContext db;

        public ReportController(ILogger<ReportController> logger, CasualEmployeeContext db)
        {
            _logger = logger;
            this.db = db;
        }


        [HttpGet]
        [Route("dashboard")]
        public IActionResult Get()
        {
            DashboardDTO result = new DashboardDTO();
            var salaries = this.GetAllSalaries();
            var groupedSalaries = salaries.GroupBy(x => new { x.First_Name, x.Last_Name }).Select(x => new SalaryDTO
            {
                Id = Guid.NewGuid(),
                First_Name = x.Key.First_Name,
                Last_Name = x.Key.Last_Name,
                Worked_Hours = x.Sum(x => x.Worked_Hours),
                Salary = x.Sum(x => x.Rate * x.Worked_Hours)
            }).OrderBy(x => x.Last_Name).ToList();

            var groupedDate = salaries.GroupBy(x => new { x.Date }).Select(x => new DailyHoursDTO
            {
                Date = x.Key.Date,
                Hours = x.Sum(x => x.Worked_Hours)
            }).OrderBy(x => x.Date).ToList();

            result.Days = groupedDate.Select(x => x.Date.ToString("dd/M/yyyy")).ToList();
            result.DailyHours = groupedDate.Select(x => x.Hours).ToList();

            result.Names = groupedSalaries.Select(x => x.First_Name + " " + x.Last_Name).ToList();
            result.NameHours = groupedSalaries.Select(x => x.Worked_Hours).ToList();
            result.Salaries = groupedSalaries.Select(x => x.Salary).ToList();
            result.totalSalaries = groupedSalaries.Take(10).ToList();

            result.Tasks = db.Tasks.Take(10).Select(x => new Tasks { Id = x.Id, Name = x.Name, isAssigned = x.Task_Assignments.Count == 0 ? false : true }).ToList();



            return this.Ok(result);
        }

        [HttpGet]
        [Route("salaries")]
        public IActionResult GetSalaries()
        {

            return this.Ok(this.GetAllSalaries());
        }

        private List<SalaryDTO> GetAllSalaries()
        {
            List<SalaryDTO> salaries = new List<SalaryDTO>();
            var salaryData = this.db.Employee_Time_Sheets.ToList();

            foreach (var entry in salaryData)
            {
                var assignment = this.db.Task_Assignments.FirstOrDefault(x => x.Id == entry.AssignedTaskId);
                var person = this.db.Employees.FirstOrDefault(x => x.Id == entry.AssigneeId);
                //var role = this.db.Roles.FirstOrDefault(x => x.Id == person.Role_Id);
                var task = this.db.Tasks.FirstOrDefault(x => x.Id == assignment.TaskId);
                salaries.Add(new SalaryDTO
                {
                    Id = entry.Id,
                    First_Name = person.Name,
                    Last_Name = person.Last_Name,
                    Role = entry.Role,
                    Task = task.Name,
                    Rate = entry.Rate,
                    Worked_Hours = entry.Worked_Hours,
                    Date = task.Date
                });
            }
            return salaries;
        }
    }
}
