using Events.API.Models.Results;
using Events.API.Models.Results.JsonResults;
using Events.API.Services.EventsServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Events.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventsController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventServices.List();

            return new EventListJsonResult(events.ToList());
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetEventsBySensor()
        {
            var events = await _eventServices.ListProcessed();

            return new EventsProcessedListJsonResult(events.ToList());
        }

        [HttpGet("by-region")]
        public async Task<IActionResult> GetEventsByRegion()
        {
            var summaries = await _eventServices.ListByRegion();

            return new RegionSummaryListJsonResult(summaries);
        }
    }
}
