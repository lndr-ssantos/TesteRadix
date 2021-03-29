using Events.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Models.Results
{
    public class EventListResult : IActionResult
    {
        public ICollection<EventResult> Events { get; set; }

        public EventListResult() { }

        public EventListResult(ICollection<Event> events)
        {
            Events = events.Select(e => new EventResult(e)).ToList();
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
