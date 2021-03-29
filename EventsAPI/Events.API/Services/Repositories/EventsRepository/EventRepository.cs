using Events.API.Infra;
using Events.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.API.Services.Repositories.EventsRepository
{
    public class EventRepository : IEventRepository
    {
        private readonly EventsDbContext _dbContext;

        public EventRepository(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddEventAsync(Event @event)
        {
            await _dbContext.Events.AddAsync(@event);
        }

        public async Task<IEnumerable<Event>> List()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
