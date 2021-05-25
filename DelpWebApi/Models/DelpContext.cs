using Microsoft.EntityFrameworkCore;

namespace DelpWebApi.Models
{
    public class DelpContext : DbContext
    {
        public DelpContext(DbContextOptions<DelpContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
    }
}