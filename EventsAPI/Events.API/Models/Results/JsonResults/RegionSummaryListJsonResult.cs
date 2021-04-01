using Events.API.Models.Results.ServicesResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.API.Models.Results.JsonResults
{
    public class RegionSummaryListJsonResult : IActionResult
    {
        public ICollection<RegionSummary> RegionsSummaries { get; set; }
        
        public RegionSummaryListJsonResult(ICollection<RegionSummary> summaries) 
        {
            RegionsSummaries = summaries;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
