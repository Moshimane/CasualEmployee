using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class Bank_Detail: NameEntityBase
    {
        public string Account_Number { get; set; }
        public string Branch_Number { get; set; }
    }
}
