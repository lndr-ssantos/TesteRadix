using Events.API.Models.Entities;
using System;

namespace Events.API.Models.ViewModels
{
    public class EventViewModel
    {
        public long TimeStamp { get; set; }
        public string Tag { get; set; }
        public string Valor { get; set; }

        public Event MapToEvent()
        {
            var eventDate = DateTimeOffset.FromUnixTimeMilliseconds(TimeStamp).DateTime.ToLocalTime();
            
            var @event = new Event(tag: Tag, eventDate: eventDate, value: Valor);
            @event.IsProcessed();

            return @event;
        }
    }
}
