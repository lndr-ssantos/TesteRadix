using System;
using System.Text.RegularExpressions;

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
        }

        public void IsProcessed()
        {
            Processed = !string.IsNullOrWhiteSpace(Value) && Regex.IsMatch(Value, @"^[0-9]+$");
        }

        public bool TagIsValid()
        {
            return Regex.IsMatch(Tag, @"^[A-Za-z]+.[A-Za-z]+.[A-Za-z]+[0-9]+$");
        }
    }
}
