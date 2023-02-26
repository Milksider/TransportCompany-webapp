using Microsoft.EntityFrameworkCore;
using TransportCompany.Models;

namespace TransportCompany.Data
{
    public class TransportCompanyDbContext : DbContext
    {
        public TransportCompanyDbContext(DbContextOptions<TransportCompanyDbContext> options) : base(options)
        { }

        public DbSet<User> users { get; set; }
        public DbSet<Trip> trips { get; set; }
        public DbSet<Transport> transports { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<TransportRoute> transportRoutes { get; set; }
        public DbSet<Driver> drivers { get; set; }
        public DbSet<Dispetcher> dispetchers { get; set; }
        public DbSet<Conductor> conductors { get; set; }
        public DbSet<BusStop> busStops { get; set; }
        public DbSet<Admin> admins { get; set; }
    }
}
