using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Employee
{
    public class Bank_DetailDTO:DTOBase
    {
        public string Account_Number { get; set; }
        public string Branch_Number { get; set; }
    }
}
