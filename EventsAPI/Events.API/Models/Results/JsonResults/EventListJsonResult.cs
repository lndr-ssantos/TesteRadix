using Events.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Models.Results.JsonResults
{
    public class EventListJsonResult : IActionResult
    {
        public ICollection<EventJsonResult> Events { get; set; }

        public EventListJsonResult() { }

        public EventListJsonResult(ICollection<Event> events)
        {
            Events = events.Select(e => new EventJsonResult(e)).ToList();
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
