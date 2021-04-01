using System.Collections.Generic;

namespace Events.API.Models.Results.ServicesResults
{
    public class RegionSummary
    {
        public RegionSummary(string region, int total, SummarySensor sensor)
        {
            Region = region;
            Total = total;
            Sensors = new List<SummarySensor>
            {
                sensor
            };
        }

        public string Region { get; set; }
        public int Total { get; set; }
        public ICollection<SummarySensor> Sensors{ get; set; }

        public void Update(SummarySensor eventAsDictionary, int total)
        {
            Total += total;
            Sensors.Add(eventAsDictionary);
        }
    }
}
