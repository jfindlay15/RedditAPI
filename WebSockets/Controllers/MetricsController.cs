using Microsoft.AspNetCore.Mvc;
using WebSockets.Helpers;
using WebSockets.Interfaces;

namespace WebSockets.Controllers
{
    public class MetricsController : BaseApiController
    {
        private readonly IMetricTracking metricTracking;

        public IMetricTracking MetricTracking => metricTracking;
        
        public MetricsController(IMetricTracking metricTracking)
        {
            this.metricTracking = metricTracking;
        }

        /// <summary>
        /// Returns a Metric Result object that contains the following:
        /// messageCount: Total messages received from Reddit since the application was started
        /// TopTen: Top 10 trending subreddits and the amount for each in the following format:   Name - count
        /// </summary>
        /// <returns></returns>
        public ActionResult<MetricResult> GetTotalMessageCount()
        {
            var count = metricTracking.GetResults();
            return Ok(count);
        }
    }
}
