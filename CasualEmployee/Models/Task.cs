using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class Task : NameEntityBase
    {
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public virtual List<Task_Assignment> Task_Assignments { get; set; }
    }
}
