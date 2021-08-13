using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Models
{
    public class EmployeeContext:IdentityDbContext<User,Role,int>
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        { 
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
