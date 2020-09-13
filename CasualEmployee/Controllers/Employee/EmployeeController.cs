using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CasualEmployee.Api.Controllers.Role;
using CasualEmployee.Api.Models;
using CasualEmployee.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasualEmployee.Api.Controllers.Employee
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CasualEmployeeContext db;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, CasualEmployeeContext db, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            this.db = db;
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("lookup")]
        public IEnumerable<EmployeeDTO> Get()
        {
            var employeeData = this.employeeRepository.GetLookups(this.db);
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            foreach (var employee in employeeData)
            {
                var data = new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Id_Number = employee.Id_Number,
                    Address = employee.Address,
                    Last_Name = employee.Last_Name,
                    Display_Picture = employee.Display_Picture
                };
                data.Bank_Detail = new Bank_DetailDTO
                {
                    Id = employee.Bank_Detail_Id,
                    Name = employee.Bank_Detail.Name,
                    Account_Number = employee.Bank_Detail.Account_Number,
                    Branch_Number = employee.Bank_Detail.Branch_Number
                };
                data.Role = new RoleDTO
                {
                    Id = employee.Role_Id,
                    Name = employee.Role.Name,
                    Rate = employee.Role.Rate
                };

                //Do the display pic convertion
                employees.Add(data);
            }
            return employees;
        }

        [HttpPost]
        [Produces("application/octet-stream")]
        [Route("download")]
        public IEnumerable<FileStreamResult> Download([FromForm] IFormFile file)
        {
            var employeeData = this.employeeRepository.GetLookups(this.db);
            List<FileStreamResult> employeeImages = new List<FileStreamResult>();
            foreach (var employee in employeeData)
            { 
                MemoryStream ms = new MemoryStream(employee.Display_Picture);
                ms.Seek(0, SeekOrigin.Begin);

                
                 employeeImages.Add(this.File(ms, "", $"image-{employee.Id}"));
            }
            return employeeImages;
        }

        [HttpGet]
        [Route("")]
        public EmployeeDTO Get([FromQuery] Guid Id)
        {
            EmployeeDTO employee = new EmployeeDTO();
            var employeeData = this.employeeRepository.GetById(Id, this.db);
            if (employeeData != null)
            {
                employee = new EmployeeDTO
                {
                    Id = employeeData.Id,
                    Name = employeeData.Name,
                    Id_Number = employeeData.Id_Number,
                    Address = employeeData.Address,
                    Last_Name = employeeData.Last_Name,
                    Bank_Name = employeeData.Bank_Detail.Name,
                    Bank_Account = employeeData.Bank_Detail.Account_Number,
                    Bank_Branch = employeeData.Bank_Detail.Branch_Number,
                    Display_Picture = employee.Display_Picture
                };
                employee.Bank_Detail = new Bank_DetailDTO
                {
                    Id = employeeData.Bank_Detail_Id,
                    Name = employeeData.Bank_Detail.Name,
                    Account_Number = employeeData.Bank_Detail.Account_Number,
                    Branch_Number = employeeData.Bank_Detail.Branch_Number
                };
                employee.Role = new RoleDTO
                {
                    Id = employeeData.Role_Id,
                    Name = employeeData.Role.Name,
                    Rate = employeeData.Role.Rate
                };
            }
            return employee;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] EmployeeDTO dto, [FromForm] IFormFile File)
        {
            return this.Ok(this.employeeRepository.Upsert(dto, this.db));
        }


        [RequestSizeLimit(50000000)]
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromForm] IFormCollection File)
        {
            this.employeeRepository.getPhotoAsync(File, this.db);
            return this.Ok();
        }


        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromQuery] Guid Id)
        {
            var employee = this.db.Employees.FirstOrDefault(x => x.Id == Id);
            //Delete from time sheet
            var times = this.db.Employee_Time_Sheets.Where(x => x.AssigneeId == Id);

            if(times != null)
            {
                this.db.Employee_Time_Sheets.RemoveRange(times);
            }
            var tasks = this.db.Task_Assignments.Where(x => x.AssigneeId == Id);
            if (tasks != null)
            {
                this.db.Task_Assignments.RemoveRange(tasks);
            }

            this.db.Employees.Remove(employee);
            this.db.SaveChanges();
            // EmployeeDTO employee = new EmployeeDTO();
            return this.Ok();
        }

        private IFormFile GetFile(byte[] photo)
        {
            try
            {
                var stream = new MemoryStream(photo);
                return new FormFile(stream, 0, photo.Length, "name", "File");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
