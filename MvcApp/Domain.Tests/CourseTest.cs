using Core.Entities;

namespace Domain.Tests
{
    [TestClass]
    public class CourseTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Course(null));
        }
    }
}
