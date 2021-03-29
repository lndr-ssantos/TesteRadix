using Events.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Models.Results
{
    public class EventsProcessedListResult : IActionResult
    {
        public ICollection<EventProcessedResult> EventsProcessed { get; set; }

        public EventsProcessedListResult() { }

        public EventsProcessedListResult(ICollection<Event> events)
        {
            EventsProcessed = new List<EventProcessedResult>();

            var eventsByTag = events.GroupBy(e => e.Tag);

            foreach (var item in eventsByTag)
            {
                EventsProcessed.Add(new EventProcessedResult(tag: item.Key, count: item.Count()));
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
