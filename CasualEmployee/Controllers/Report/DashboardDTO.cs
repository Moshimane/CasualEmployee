using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Report
{
    public class DashboardDTO
    {
        public List<string> Names { get; set; } = new List<string>();
        public List<double> Salaries { get; set; } = new List<double>();
        public List<int> DailyHours { get; set; } = new List<int>();
        public List<string> Days { get; set; } = new List<string>();
        public List<int> NameHours { get; set; } = new List<int>();
        public List<SalaryDTO> totalSalaries { get; set; } = new List<SalaryDTO>();
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();
    }

    public class DailyHoursDTO 
    {
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }

    public class Tasks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool isAssigned { get; set; }
    }
}
