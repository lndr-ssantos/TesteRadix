using Events.API.Models.Entities;
using System;

namespace Events.API.Models.Results
{
    public class EventJsonResult
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public bool Processed { get; set; }

        public EventJsonResult() { }

        public EventJsonResult(Event e)
        {
            Tag = e.Tag;
            Date = e.EventDate;
            Value = e.Value;
            Processed = e.Processed;
        }
    }
}
