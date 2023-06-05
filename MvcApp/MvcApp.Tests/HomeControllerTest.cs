using MvcApp.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Core.DataSource;

namespace MvcApp.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            var mockUnitToWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<HomeController>>();

            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(null, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(mockUnitToWork.Object, null));
            Assert.ThrowsException<ArgumentNullException>(() => new HomeController(null, null));
        }
    }
}
