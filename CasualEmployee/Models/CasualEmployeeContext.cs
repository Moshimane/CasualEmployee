using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class CasualEmployeeContext: DbContext
    {
        public CasualEmployeeContext(DbContextOptions<CasualEmployeeContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task_Assignment> Task_Assignments { get; set; }
        public DbSet<Employee_Time_Sheet> Employee_Time_Sheets { get; set; }
        public DbSet<Bank_Detail> Bank_Details { get; set; }

    }
}

