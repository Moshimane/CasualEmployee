using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualEmployee.Api.Models;
using CasualEmployee.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasualEmployee.Api.Controllers.Task
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly CasualEmployeeContext db;
        private readonly ITaskRepository taskRepository;

        public TaskController(ILogger<TaskController> logger, CasualEmployeeContext db, ITaskRepository taskRepository)
        {
            _logger = logger;
            this.db = db;
            this.taskRepository = taskRepository;
        }

        [HttpGet]
        [Route("lookup")]
        public IEnumerable<TaskDTO> Get()
        {
            var taskData = this.taskRepository.GetLookups(this.db);
            List<TaskDTO> tasks = new List<TaskDTO>();
            foreach (var task in taskData)
            {
                tasks.Add(this.GetAssignedTasks(task, new TaskDTO
                {
                    Id = task.Id,
                    Name = task.Name,
                    Date = task.Date,
                    Description = task.Description,
                    Duration = task.Duration,
                    Status = task.Status
                }));
            }
            return tasks;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get([FromQuery] Guid Id)
        {
            TaskDTO task = new TaskDTO();
            try
            {
                var taskData = this.taskRepository.GetById(Id, this.db);

                task = this.GetAssignedTasks(taskData, new TaskDTO
                {
                    Id = taskData.Id,
                    Name = taskData.Name,
                    Date = taskData.Date,
                    Description = taskData.Description,
                    Duration = taskData.Duration,
                    Status = taskData.Status
                });

                return this.Ok(task);
            }
            catch(Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] TaskDTO dto)
        {
            try
            {
                this.taskRepository.Upsert(dto, this.db);
                return this.Ok();
            }
            catch(Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromQuery] Guid Id)
        {
            var task = this.db.Tasks.FirstOrDefault(x => x.Id == Id);
            var taskAssignments = this.db.Task_Assignments.Where(x => x.TaskId == Id);
            foreach (var assignment in taskAssignments) 
            {
                //Delete from time sheet
                var times = this.db.Employee_Time_Sheets.Where(x => x.AssignedTaskId == assignment.Id);
                if (times != null)
                {
                    this.db.Employee_Time_Sheets.RemoveRange(times);
                }
                this.db.Task_Assignments.Remove(assignment);
            }
          
            this.db.Tasks.Remove(task);
            this.db.SaveChanges();

            return this.Ok();
        }

        [HttpGet]
        [Route("unassign")]
        public IActionResult UnAssign([FromQuery] Guid Id)
        {
            var assignment = this.db.Task_Assignments.FirstOrDefault(x => x.Id == Id);
            var times = this.db.Employee_Time_Sheets.Where(x => x.AssignedTaskId == Id);
            if(times != null)
            {
                this.db.Employee_Time_Sheets.RemoveRange(times);
            }
            this.db.Task_Assignments.Remove(assignment);
            this.db.SaveChanges();
            return this.Ok();
        }

        private TaskDTO GetAssignedTasks(Models.Task task, TaskDTO result)
        {
            if (task.Task_Assignments != null)
            {
                var task_Assignments = task.Task_Assignments;
                result.AssigneeNames = new List<string>();
                result.task_Assignments = new List<Task_AssignmentDTO>();
                foreach (var assigned in task_Assignments)
                {
                    result.AssigneeNames.Add(db.Employees.FirstOrDefault(x => x.Id == assigned.AssigneeId).Name);
                    result.task_Assignments.Add(new Task_AssignmentDTO 
                    {
                        Id = assigned.Id,
                        AssigneeId = assigned.AssigneeId,
                        Date = assigned.Date,
                        TaskId = assigned.TaskId,
                        Hours = assigned.Hours
                    });
                }
                return result;
            }

            return null;
        }
    }
}
