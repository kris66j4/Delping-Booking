using Microsoft.EntityFrameworkCore;

using Delpin_Booking.Models;

namespace Delpin_Booking.Data
{
    public class DelpinBookingContext
    {

        public DelpinBookingContext(DbContextOptions<DelpinBookingContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}

