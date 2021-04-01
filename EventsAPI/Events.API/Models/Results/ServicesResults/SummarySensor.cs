namespace Events.API.Models.Results.ServicesResults
{
    public class SummarySensor
    {
        public SummarySensor(string name, string total)
        {
            Name = name;
            Total = total;
        }

        public string Name { get; set; }
        public string Total { get; set; }
    }
}
