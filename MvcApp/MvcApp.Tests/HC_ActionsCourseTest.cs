using Core.DataSource;
using Core.Entities;
using Core.ValueObjects;
using Core.Repositoties;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MvcApp.Tests
{
    [TestClass]
    public class HC_ActionsCourseTest
    {
        [TestMethod]
        public async Task IndexViewResultNotNull()
        {
            // Arrange
            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup((p) => p.GetAll()).ReturnsAsync(new List<Course>() { new Course(new DataCourse("name", "")) });

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_ModelError()
        {
            //Arrenge
            string expected = "CreateCourse";

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Create(new Course(new DataCourse("name", ""))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            CourseModel courseModel = new CourseModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("COURSE_ID", "COURSE_ID not setup!");

            //Act
            ViewResult result = await controller.CreateCourse(courseModel) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_RedirectToIndexView()
        {
            //Arrange
            string expected = "Index";

            CourseModel courseModel = new CourseModel()
            {
                NAME = "name",
                DESCRIPTION = ""
            };

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Create(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.CreateCourse(courseModel) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_SaveModel()
        {
            //Arrange
            CourseModel courseModel = new CourseModel()
            {
                NAME = "name",
                DESCRIPTION = ""
            };

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Create(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);            

            //Act
            var result = await controller.CreateCourse(courseModel) as RedirectToActionResult;

            //Assert
            mockCourseRepository.Verify(a => a.Create(It.IsAny<Course>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task EditCourseViewResultNotNull()
        {
            // Arrange
            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup((p) => p.Get(It.IsAny<int?>())).ReturnsAsync(new Course(new DataCourse("name", "")));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            // Act
            var result = await controller.EditCourse(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditCoursePostAction_ModelError()
        {
            //Arrenge
            string expected = "EditCourse";

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Update(new Course(new DataCourse("name", ""))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            CourseModel courseModel = new CourseModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("COURSE_ID", "COURSE_ID not setup!");

            //Act
            var result = await controller.EditCourse(courseModel) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditCoursePostAction_RedirectToIndexView()
        {
            //Arrange
            string expected = "Index";

            CourseModel courseModel = new CourseModel()
            {
                COURSE_ID = 1,
                NAME = "name",
                DESCRIPTION = ""
            };

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Course(new DataCourse("n", "d")));
            mockCourseRepository.Setup(m => m.Update(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.EditCourse(courseModel) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditCoursePostAction_SaveModel()
        {
            //Arrange
            CourseModel courseModel = new CourseModel()
            {
                NAME = "name",
                DESCRIPTION = ""
            };

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Course(new DataCourse("n", "d")));
            mockCourseRepository.Setup(m => m.Update(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.EditCourse(courseModel) as RedirectToActionResult;

            //Assert
            mockCourseRepository.Verify(a => a.Update(It.IsAny<Course>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task DeleteCoursePostAction_RedirectToIndexView()
        {
            string expected = "Index";

            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.GetWhere(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(new List<Course>() { new Course(new DataCourse("n", "d")) });
            mockCourseRepository.Setup(m => m.Delete(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();

            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteCourse(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteCoursePostAction_SaveModel()
        {
            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup(m => m.GetWhere(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(new List<Course>() { new Course(new DataCourse("n", "d")) });
            mockCourseRepository.Setup(m => m.Delete(It.IsAny<Course>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();

            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteCourse(1) as RedirectToActionResult;

            mockCourseRepository.Verify(a => a.Delete(It.IsAny<Course>()));
            mockUnitToWork.Verify(a => a.Save());
        }
    }
}