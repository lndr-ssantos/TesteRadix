using System;

namespace Events.API.Models.Entities
{
    public class Event
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public DateTime EventDate { get; set; }
        public string Value { get; set; }
        public bool Processed { get; set; }

        public Event() { }

        public Event(string tag, DateTime eventDate, string value)
        {
            Tag = tag;
            EventDate = eventDate;
            Value = value;
            Processed = !string.IsNullOrWhiteSpace(Value);
        }

        public void IsProcessed()
        {
            Processed = string.IsNullOrWhiteSpace(Value);
        }
    }
}
