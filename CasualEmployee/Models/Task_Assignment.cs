using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class Task_Assignment : EntityBase
    {
        public Guid TaskId { get; set; }
        public Guid AssigneeId { get; set; }
        /// <summary>
        ///     Gets or sets the employee details.
        /// </summary>
        /// <value>The employee details.</value>
        public virtual Employee Assignee { get; set;}
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
