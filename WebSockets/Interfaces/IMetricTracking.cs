using WebSockets.Helpers;

namespace WebSockets.Interfaces
{
    public interface IMetricTracking
    {
        MetricResult GetResults();
        void AddSubreddit(string subreddit);        
    }
}
