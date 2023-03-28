using Microsoft.AspNetCore.Mvc;
using Moq;
using WebSockets.Controllers;
using WebSockets.Helpers;
using WebSockets.Interfaces;
using Assert = NUnit.Framework.Assert;

namespace JackHenryTest.Tests
{
    [TestClass]
    public class MetricsControllerTests
    {
        [TestMethod]
        public void GetTotalMessageCount_ReturnsCorrectCount()
        {
            // Arrange
            var mockMetricTracking = new Mock<IMetricTracking>();
            mockMetricTracking.Setup(m => m.GetResults())
                              .Returns(new MetricResult { MessageCount = 42, TopTen = new List<string>() });

            var controller = new MetricsController(mockMetricTracking.Object);

            // Act
            var result = controller.GetTotalMessageCount();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult?)result.Result;
            Assert.AreEqual(42, ((MetricResult?)okResult?.Value)?.MessageCount);
        }


        [TestMethod]
        public void MetricsController_CorrectlyAssignsMetricTracking()
        {
            // Arrange
            var mockMetricTracking = new Mock<IMetricTracking>();

            // Act
            var controller = new MetricsController(mockMetricTracking.Object);

            // Assert
            Assert.AreEqual(mockMetricTracking.Object, controller.MetricTracking);
        }
    }
}
