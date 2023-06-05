using Core.DataSource;
using Core.Entities;
using Core.ValueObjects;
using Core.Repositoties;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using Microsoft.Extensions.Logging;

namespace MvcApp.Tests
{
    [TestClass]
    public class HC_ActionStudentTest
    {
        [TestMethod]
        public async Task StudentsOfGroupViewResultNotNull()
        {
            // Arrange
            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup((p) => p.Get(1)).ReturnsAsync(new Group(1, new Name("name")));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            // Act
            var result = await controller.StudentsOfGroup(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateStudentPostAction_ModelError()
        {
            //Arrenge
            string expected = "CreateStudent";

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Create(new Student(1, new StudentName(new Name("fname"), new Name("lname")))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            StudentModel studentModel = new StudentModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("STUDENT_ID", "STUDENT_ID not setup!");

            //Act
            ViewResult result = await controller.CreateStudent(studentModel) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_RedirectToGroupOfCourseView()
        {
            //Arrange
            string expected = "StudentsOfGroup";

            StudentModel studentModel = new StudentModel()
            {
                STUDENT_ID = 1,
                GROUP_ID = 1,
                FIRST_NAME = "fname",
                LAST_NAME = "lname"
            };

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Create(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.CreateStudent(studentModel) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_SaveModel()
        {
            //Arrange
            StudentModel studentModel = new StudentModel()
            {
                STUDENT_ID = 1,
                GROUP_ID = 1,
                FIRST_NAME = "fname",
                LAST_NAME = "lname"
            };

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Create(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.CreateStudent(studentModel) as RedirectToActionResult;

            //Assert
            mockStudentRepository.Verify(a => a.Create(It.IsAny<Student>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task EditStudentViewResultNotNull()
        {
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditStudent(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditStudentPostAction_ModelError()
        {
            string expected = "EditStudent";

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));
            mockStudentRepository.Setup(m => m.Update(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            StudentModel studentModel = new StudentModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("STUDENT_ID", "STUDENT_ID not setup!");

            var result = await controller.EditStudent(studentModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_RedirectToGroupsOfCourse()
        {
            string expected = "StudentsOfGroup";

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));
            mockStudentRepository.Setup(m => m.Update(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            StudentModel studentModel = new StudentModel()
            {
                STUDENT_ID = 1,
                GROUP_ID = 1,
                FIRST_NAME = "fname",
                LAST_NAME = "lname"
            };

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditStudent(studentModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_SaveModel()
        {
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));
            mockStudentRepository.Setup(m => m.Update(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            StudentModel studentModel = new StudentModel()
            {
                STUDENT_ID = 1,
                GROUP_ID = 1,
                FIRST_NAME = "fname",
                LAST_NAME = "lname"
            };

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditStudent(studentModel) as RedirectToActionResult;

            mockStudentRepository.Verify(a => a.Update(It.IsAny<Student>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_RedirectToGroupsOfCourse()
        {
            string expected = "StudentsOfGroup";

            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));
            mockStudentRepository.Setup(m => m.Delete(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteStudent(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_SaveModel()
        {
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Student(1, new StudentName(new Name("fname"), new Name("lname"))));
            mockStudentRepository.Setup(m => m.Delete(It.IsAny<Student>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Students).Returns(mockStudentRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteStudent(1) as RedirectToActionResult;

            mockStudentRepository.Verify(a => a.Delete(It.IsAny<Student>()));
            mockUnitToWork.Verify(a => a.Save());
        }
    }
}
