using PruebaUnoAgioGlobal.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PruebaUnoAgioGlobal.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Airline> Airlines { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}