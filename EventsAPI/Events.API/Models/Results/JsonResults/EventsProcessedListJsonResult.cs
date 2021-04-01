using Events.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Models.Results
{
    public class EventsProcessedListJsonResult : IActionResult
    {
        public ICollection<EventProcessedJsonResult> EventsProcessed { get; set; }

        public EventsProcessedListJsonResult() { }

        public EventsProcessedListJsonResult(ICollection<Event> events)
        {
            EventsProcessed = new List<EventProcessedJsonResult>();

            var eventsByTag = events.GroupBy(e => e.Tag);

            foreach (var item in eventsByTag)
            {
                EventsProcessed.Add(new EventProcessedJsonResult(tag: item.Key, count: item.Count()));
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
