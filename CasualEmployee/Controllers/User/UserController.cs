using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasualEmployee.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasualEmployee.Api.Controllers.User
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;
        private readonly CasualEmployeeContext db;

        public UserController(ILogger<UserController> logger, CasualEmployeeContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var data = this.db.Users.ToList();
            return data;
        }
    }
}
