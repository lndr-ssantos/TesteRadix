using Events.API.Models.Entities;
using Events.API.Models.ViewModels;
using Events.API.Services.Repositories.EventsRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Services.EventsServices
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepository _eventsRepository;

        public EventServices(IEventRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task ProcessEvents(EventViewModel eventViewModel)
        {
            var @event = eventViewModel.MapToEvent();

            await _eventsRepository.AddEventAsync(@event);

            await _eventsRepository.SaveAsync();
        }

        public async Task<List<Event>> List()
        {
            var events = await _eventsRepository.List();
            return events.ToList();
        }

        public async Task<List<Event>> ListProcessed()
        {
            var events = await _eventsRepository.ListProcessed();
            return events.ToList();
        }
    }
}
