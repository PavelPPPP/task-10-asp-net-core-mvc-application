using Core.ValueObjects;

namespace Domain.Tests
{
    [TestClass]
    public class DataCourseTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DataCourse(null, null));
        }

        public void WhenConstructorParamIsEmpty_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DataCourse(" ", null));
        }
    }
}
