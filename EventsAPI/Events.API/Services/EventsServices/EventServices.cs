using Events.API.Infra.Concurrency;
using Events.API.Models.Entities;
using Events.API.Models.Results;
using Events.API.Models.Results.ServicesResults;
using Events.API.Models.ViewModels;
using Events.API.Services.Repositories.EventsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Services.EventsServices
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepository _eventsRepository;
        private static readonly AsyncLock _asyncLock = new AsyncLock();

        public EventServices(IEventRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task ProcessEvents(EventViewModel eventViewModel)
        {
            using (await _asyncLock.LockAsync())
            {
                var @event = eventViewModel.MapToEvent();

                if (!@event.TagIsValid())
                {
                    throw new ArgumentException($"Tag inválida.\n\nTag recebida: {@event.Tag}");
                }

                await _eventsRepository.AddEventAsync(@event);

                await _eventsRepository.SaveAsync();
            }
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

        public async Task<List<RegionSummary>> ListByRegion()
        {
            var events = await _eventsRepository.ListProcessed();

            var eventsGroupedByTag = events.GroupBy(e => e.Tag);

            var regionSummaries = new List<RegionSummary>();

            foreach (var eventGroup in eventsGroupedByTag)
            {
                GetSummary(regionSummaries, eventGroup);
            }

            return regionSummaries;
        }

        private void GetSummary(List<RegionSummary> regionSummaries, IGrouping<string, Event> eventGroup)
        {
            var tagSplited = eventGroup.Key.Split('.');
            var region = $"{tagSplited[0]}.{tagSplited[1]}";
            var total = eventGroup.Count();
            var eventAsDictionary = new SummarySensor(name: eventGroup.Key, total: total.ToString());

            var existingRegionSummary = regionSummaries.SingleOrDefault(x => x.Region == region);

            if (existingRegionSummary != null)
            {
                existingRegionSummary.Update(eventAsDictionary, total);
            }
            else
            {
                regionSummaries.Add(new RegionSummary(
                    region: region, 
                    total: total,
                    eventAsDictionary));
            }
        }
    }
}
