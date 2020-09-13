using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Task
{
    public class TaskDTO: DTOBase
    {
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public List<Task_AssignmentDTO> task_Assignments { get; set; } = new List<Task_AssignmentDTO>();
        public List<string> AssigneeNames { get; set; }
        public string Status { get; set; }
    }
}
