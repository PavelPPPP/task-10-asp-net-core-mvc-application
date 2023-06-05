using Core.Entities;
using Core.ValueObjects;

namespace Domain.Tests
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Group(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Group(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Group(null, new Name("name")));
        }

        [TestMethod]
        public void WhenConstructorParamIsNotValid_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Group(0, new Name("name")));
            Assert.ThrowsException<ArgumentException>(() => new Group(-1, new Name("name")));
        }
    }
}
