using WebSockets.DTOs;
using WebSockets.Interfaces;

namespace WebSockets
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMetricTracking metricTracking;

        public MessageProcessor(IMetricTracking metricTracking)
        {
            this.metricTracking = metricTracking;
        }

        public void ProcessMessages(List<Comment> comments)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(comments, options, comment =>
            {
                ProcessMessages(comment);
            });
        }

        private void ProcessMessages(Comment comment)
        {
            if(!string.IsNullOrEmpty(comment.Subreddit))
            {
                metricTracking.AddSubreddit(comment.Subreddit);
            }            
        }
    }
}
