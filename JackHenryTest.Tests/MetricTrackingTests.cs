using WebSockets;

namespace JackHenryTest.Tests
{
    [TestClass]
    public class MetricTrackingTests
    {
        [TestMethod]
        public void AddSubreddit_AddsToDictionary()
        {
            // Arrange
            var metricTracking = new MetricTracking();

            // Act
            metricTracking.AddSubreddit("testsubreddit");

            // Assert
            Assert.IsTrue(metricTracking.GetResults().TopTen.Contains("testsubreddit - 1"));
        }        

        [TestMethod]
        public void AddSubreddit_IncrementsExistingCount()
        {
            // Arrange
            var metricTracking = new MetricTracking();
            metricTracking.AddSubreddit("testsubreddit");

            // Act
            metricTracking.AddSubreddit("testsubreddit");

            // Assert
            Assert.IsTrue(metricTracking.GetResults().TopTen.Contains("testsubreddit - 2"));
        }       

        [TestMethod]
        public void GetResults_ReturnsTopTenSubreddits()
        {
            // Arrange
            var metricTracking = new MetricTracking();
            metricTracking.AddSubreddit("subreddit1");
            metricTracking.AddSubreddit("subreddit2");
            metricTracking.AddSubreddit("subreddit3");
            metricTracking.AddSubreddit("subreddit4");
            metricTracking.AddSubreddit("subreddit5");
            metricTracking.AddSubreddit("subreddit6");
            metricTracking.AddSubreddit("subreddit7");
            metricTracking.AddSubreddit("subreddit8");
            metricTracking.AddSubreddit("subreddit9");
            metricTracking.AddSubreddit("subreddit10");            

            // Act
            var result = metricTracking.GetResults();

            // Assert
            Assert.AreEqual(result.TopTen.Count, 10);
            Assert.IsTrue(result.TopTen.Contains("subreddit1 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit2 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit3 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit4 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit5 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit6 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit7 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit8 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit9 - 1"));
            Assert.IsTrue(result.TopTen.Contains("subreddit10 - 1"));
        }
    }
}
