using Homework09_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Homework09_API.Data
{
    public class CarBookingDbContext : DbContext
    {
        public DbSet<Car> Car { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public CarBookingDbContext(DbContextOptions<CarBookingDbContext> options) : base(options) { }
    }
}
