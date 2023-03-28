using System.Collections.Concurrent;
using WebSockets.Helpers;
using WebSockets.Interfaces;

namespace WebSockets
{
    public class MetricTracking : IMetricTracking
    {
        private static int messageCount = 0;
        private static readonly object dictionaryLock = new();

        private ConcurrentDictionary<string, int> subredditDictionary = new();

        /// <summary>
        /// Add the subreddit id to the dictionary.  This will also incremenet the value of the dictionary to keep track of the count.
        /// </summary>
        /// <param name="subreddit"></param>
        public void AddSubreddit(string subreddit)
        {
            subredditDictionary.AddOrUpdate(subreddit, 1, (_, count) => count + 1);
            IncrementCount();
        }

        private List<string> TopSubreddits()
        {
            var subreddits = subredditDictionary.OrderByDescending(kv => kv.Value).Take(10).Select(kv => $"{kv.Key} - {kv.Value}").ToList();
            return subreddits;
        }


        private void IncrementCount()
        {
            lock (dictionaryLock)
            {
                Interlocked.Increment(ref messageCount);
            }
        }
        private int GetCount()
        {
            lock (dictionaryLock)
            {
                var count = Interlocked.CompareExchange(ref messageCount, 0, 0);
                return count;
            }
        }

        public MetricResult GetResults()
        {
            var messageCount = GetCount();
            var top10 = TopSubreddits();
            var results = new MetricResult { MessageCount = messageCount, TopTen = top10 };
            return results;
        }
    }
}
