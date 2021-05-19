using Microsoft.EntityFrameworkCore;
using Delpin_Booking.Models;

namespace Delpin_Booking.Data
{
    public class DelpinBookingContext : DbContext
    {
        public DelpinBookingContext(DbContextOptions<DelpinBookingContext> options)
            : base(options)
        {
        }

        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}