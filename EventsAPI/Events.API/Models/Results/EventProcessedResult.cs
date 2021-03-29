namespace Events.API.Models.Results
{
    public class EventProcessedResult
    {
        public string Tag { get; set; }
        public int Count { get; set; }

        public EventProcessedResult() { }

        public EventProcessedResult(string tag, int count)
        {
            Tag = tag;
            Count = count;
        }
    }
}
