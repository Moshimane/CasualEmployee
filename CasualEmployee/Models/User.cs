using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class User: EntityBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

    }
}
