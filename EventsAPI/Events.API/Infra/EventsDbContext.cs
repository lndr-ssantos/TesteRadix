using Events.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.API.Infra
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
        }
    }
}
