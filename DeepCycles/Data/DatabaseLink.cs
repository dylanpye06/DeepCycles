using DeepCycles.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeepCycles.Data
{
    public class DataBaseLink(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HandmadeBikes> HandmadeBikes { get; set; }
    }
}
