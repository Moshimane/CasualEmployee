using CasualEmployee.Api.Controllers.Employee;
using CasualEmployee.Api.Controllers.Task;
using CasualEmployee.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        public IList<Models.Task> GetLookups(CasualEmployeeContext db)
        {
            var result = new List<Models.Task>();
            var tasks = db.Tasks.ToList();
            foreach(var task in tasks)
            {
                task.Task_Assignments = this.GetAssignedTasks(db.Task_Assignments.Where(x => x.TaskId == task.Id).ToList(), db);
                result.Add(task);
            }

            return result;
        }

        public Models.Task GetById(Guid Id, CasualEmployeeContext db)
        {
           var task = db.Tasks.FirstOrDefault(x => x.Id == Id);
            task.Task_Assignments = this.GetAssignedTasks(db.Task_Assignments.Where(x => x.TaskId == task.Id).ToList(), db);
            
            return task;
        }

        public void Upsert(TaskDTO task, CasualEmployeeContext db)
        {
            var exists = db.Tasks.Any(x => x.Id == task.Id);
            if (exists)
            {
                var entity = db.Tasks.FirstOrDefault(x => x.Id == task.Id);
                entity.Name = task.Name;
                entity.Date = task.Date;
                entity.Description = task.Description;
                entity.Duration = task.Duration;
                entity.Status = task.Status;
                this.MapTasks(entity, task.task_Assignments, db);
                db.Tasks.Update(entity);
                db.SaveChanges();
            }
            else
            {
                var entity = new Models.Task();
                entity.Id = Guid.NewGuid();
                entity.Name = task.Name;
                entity.Date = task.Date;
                entity.Description = task.Description;
                entity.Duration = task.Duration;
                entity.Status = task.Status;
                this.MapTasks(entity, task.task_Assignments, db);
                db.Tasks.Add(entity);
                db.SaveChanges();

            }
        }

        private void MapTasks(Models.Task task, List<Task_AssignmentDTO> task_Assignments, CasualEmployeeContext db)
        {
            if(task_Assignments != null)
            {
                foreach (var assignment in task_Assignments)
                {
                    var exists = db.Task_Assignments.Any(x => x.Id == assignment.Id);
                    if (exists)
                    {
                        var entity = db.Task_Assignments.FirstOrDefault(x => x.Id == assignment.Id);
                        entity.TaskId = task.Id;
                        entity.Date = assignment.Date;
                        entity.AssigneeId = assignment.AssigneeId;
                        entity.Hours = assignment.Hours;
                        this.MapTimeSheet(task, entity, db);
                        db.Task_Assignments.Update(entity);
                    }
                    else
                    {
                        var entity = new Task_Assignment();
                        entity.Id = Guid.NewGuid();
                        entity.TaskId = task.Id;
                        entity.Date = assignment.Date;
                        entity.Hours = assignment.Hours;
                        entity.AssigneeId = assignment.AssigneeId;
                        this.MapTimeSheet(task, entity, db);
                        db.Task_Assignments.Add(entity);
                    }
                }
            }
        }

        private void MapTimeSheet(Models.Task task, Task_Assignment assignment, CasualEmployeeContext db)
        {
            var exists = db.Employee_Time_Sheets.Any(x => x.AssignedTaskId == assignment.Id && x.AssigneeId == assignment.AssigneeId);
            var role_Id = db.Employees.FirstOrDefault(x => x.Id == assignment.AssigneeId).Role_Id;
            var role = db.Roles.FirstOrDefault(x => x.Id == role_Id);
            if (exists)
            {
                var entity = db.Employee_Time_Sheets.FirstOrDefault(x => x.AssignedTaskId == assignment.Id && x.AssigneeId == assignment.AssigneeId);
                entity.Worked_Hours = assignment.Hours;
                entity.Rate = role.Rate;
                entity.Role = role.Name;
                db.Employee_Time_Sheets.Update(entity);
            }
            else
            {
                var entity = new Employee_Time_Sheet();
                entity.Id = Guid.NewGuid();
                entity.Worked_Hours = assignment.Hours;
                entity.Rate = role.Rate;
                entity.Role = role.Name;
                entity.AssignedTaskId = assignment.Id;
                entity.AssigneeId = assignment.AssigneeId;
                db.Employee_Time_Sheets.Add(entity);
            }
        }

        private List<Task_Assignment> GetAssignedTasks(List<Task_Assignment> task_Assignments, CasualEmployeeContext db)
        {
            if (task_Assignments != null)
            {
                var assignedTasks = new List<Task_Assignment>();
                foreach (var assigned in task_Assignments)
                {
                    assigned.Assignee = db.Employees.FirstOrDefault(x => x.Id == assigned.AssigneeId);
                    assignedTasks.Add(assigned);
                }
                return assignedTasks;
            }

            return null;
        }
    }
}
