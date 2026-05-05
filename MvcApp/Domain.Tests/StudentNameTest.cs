using Core.ValueObjects;

namespace Domain.Tests
{
    [TestClass]
    public class StudentNameTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(null, new Name("name")));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(new Name("name"), null));
        }

        [TestMethod]
        public void WhenConstructorParamIsEmpty_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(new Name(" "), new Name(" ")));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(new Name(" "), new Name("name")));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentName(new Name("name"), new Name(" ")));
        }
    }
}
