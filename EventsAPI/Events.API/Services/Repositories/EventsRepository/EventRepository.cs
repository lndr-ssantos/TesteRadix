using Events.API.Infra;
using Events.API.Models.Entities;
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
            await _dbContext.AddAsync(@event);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
