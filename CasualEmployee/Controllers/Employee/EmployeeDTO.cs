using CasualEmployee.Api.Controllers.Role;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Controllers.Employee
{
    public class EmployeeDTO:DTOBase
    {
        public string Id_Number { get; set; }

        public string Last_Name { get; set; }

        public string Address { get; set; }

        public string Bank_Name { get; set; }
        public string Bank_Account { get; set; }
        public string Bank_Branch { get; set; }

        public Bank_DetailDTO Bank_Detail { get; set; }
        
        public RoleDTO Role { get; set; }

        public byte[] Display_Picture { get; set; }
    }
}
