using Moq;
using WebSockets.Interfaces;
using WebSockets;
using WebSockets.DTOs;

namespace JackHenryTest.Tests
{
    [TestClass]
    public class MessageProcessorTests
    {
        [TestMethod]
        public void ProcessMessages_AddsSubredditsToMetricTracking()
        {
            // Arrange
            var comments = new List<Comment>
        {
            new Comment { Subreddit = "testsubreddit1" },
            new Comment { Subreddit = "testsubreddit2" },
            new Comment { Subreddit = "testsubreddit3" }
        };
            var mockMetricTracking = new Mock<IMetricTracking>();
            var messageProcessor = new MessageProcessor(mockMetricTracking.Object);

            // Act
            messageProcessor.ProcessMessages(comments);

            // Assert
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit1"), Times.Once);
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit2"), Times.Once);
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit3"), Times.Once);
        }

        [TestMethod]
        public void ProcessMessages_DoesNotAddNullSubredditToMetricTracking()
        {
            // Arrange
            var comments = new List<Comment>
        {
            new Comment { Subreddit = "testsubreddit1" },
            new Comment { Subreddit = null },
            new Comment { Subreddit = "testsubreddit3" }
        };
            var mockMetricTracking = new Mock<IMetricTracking>();
            var messageProcessor = new MessageProcessor(mockMetricTracking.Object);

            // Act
            messageProcessor.ProcessMessages(comments);

            // Assert
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit1"), Times.Once);
            mockMetricTracking.Verify(mt => mt.AddSubreddit(null), Times.Never);
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit3"), Times.Once);
        }

        [TestMethod]
        public void ProcessMessages_DoesNotAddEmptySubredditToMetricTracking()
        {
            // Arrange
            var comments = new List<Comment>
        {
            new Comment { Subreddit = "testsubreddit1" },
            new Comment { Subreddit = "" },
            new Comment { Subreddit = "testsubreddit3" }
        };
            var mockMetricTracking = new Mock<IMetricTracking>();
            var messageProcessor = new MessageProcessor(mockMetricTracking.Object);

            // Act
            messageProcessor.ProcessMessages(comments);

            // Assert
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit1"), Times.Once);
            mockMetricTracking.Verify(mt => mt.AddSubreddit(""), Times.Never);
            mockMetricTracking.Verify(mt => mt.AddSubreddit("testsubreddit3"), Times.Once);
        }
    }
}
