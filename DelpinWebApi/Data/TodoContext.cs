using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DelpinWebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ressource> Ressources { get; set; }

    }
}
