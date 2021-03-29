using Events.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.API.Services.Repositories.EventsRepository
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event @event);
        Task<IEnumerable<Event>> List();
        Task SaveAsync();
    }
}
