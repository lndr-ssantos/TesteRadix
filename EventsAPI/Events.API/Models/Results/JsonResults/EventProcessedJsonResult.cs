namespace Events.API.Models.Results
{
    public class EventProcessedJsonResult
    {
        public string Tag { get; set; }
        public int Count { get; set; }

        public EventProcessedJsonResult() { }

        public EventProcessedJsonResult(string tag, int count)
        {
            Tag = tag;
            Count = count;
        }
    }
}
