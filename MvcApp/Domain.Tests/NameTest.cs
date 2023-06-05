using Core.ValueObjects;

namespace Domain.Tests
{
    [TestClass]
    public class NameTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Name(null));
        }

        [TestMethod]
        public void WhenConstructorParamIsEmpty_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Name("  "));
        }
    }
}