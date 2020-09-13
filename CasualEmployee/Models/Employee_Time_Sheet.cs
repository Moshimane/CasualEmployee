using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class Employee_Time_Sheet: EntityBase
    {
        public Guid AssignedTaskId { get; set; }

        public Guid AssigneeId { get; set; }
        /// <summary>
        ///     Gets or sets the task.
        /// </summary>
        /// <value>The task details.</value>
        public virtual Task_Assignment Task_Assignment { get; set; } 

	    public int Worked_Hours { get; set; }
	    public double Rate { get; set; }
        public string Role { get; set; }
    }
}
