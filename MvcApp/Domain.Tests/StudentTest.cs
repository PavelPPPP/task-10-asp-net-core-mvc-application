using Core.Entities;
using Core.ValueObjects;

namespace Domain.Tests
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Student(null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Student(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => new Student(null, new StudentName(new Name("name"), new Name("name"))));
        }

        [TestMethod]
        public void WhenConstructorParamIsNotValid_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Student(0, new StudentName(new Name("name"), new Name("name"))));
            Assert.ThrowsException<ArgumentException>(() => new Student(-1, new StudentName(new Name("name"), new Name("name"))));
        }
    }
}
