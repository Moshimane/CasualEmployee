using CasualEmployee.Api.Controllers.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Task
{
    public class Task_AssignmentDTO
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid AssigneeId { get; set; }
        /// <summary>
        ///     Gets or sets the employee details.
        /// </summary>
        /// <value>The employee details.</value>
        public EmployeeDTO Assignee { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
