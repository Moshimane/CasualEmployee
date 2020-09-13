using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Report
{
    public class SalaryDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Role { get; set; }
        public string Task { get; set; }
        public int Worked_Hours { get; set; }
        public double Rate { get; set; }
        public double Salary { get; set; }
    }
}
