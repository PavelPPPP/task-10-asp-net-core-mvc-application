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
    public class HC_ActionGroupTest
    {
        [TestMethod]
        public async Task GroupsOfCourseViewResultNotNull()
        {
            // Arrange
            var mockCourseRepository = new Mock<IRepository<Course>>();
            mockCourseRepository.Setup((p) => p.Get(1)).ReturnsAsync(new Course(new DataCourse("name", "")));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Courses).Returns(mockCourseRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            // Act
            var result = await controller.GroupsOfCourse(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_ModelError()
        {
            //Arrenge
            string expected = "CreateGroup";

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Create(new Group(1, new Name("name"))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            GroupModel groupeModel = new GroupModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("GROUP_ID", "GROUP_ID not setup!");

            //Act
            ViewResult result = await controller.CreateGroup(groupeModel) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_RedirectToGroupOfCourseView()
        {
            //Arrange
            string expected = "GroupsOfCourse";

            GroupModel groupModel = new GroupModel()
            {
                GROUP_ID = 1,
                COURSE_ID = 1,
                NAME = "name"
            };

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Create(It.IsAny<Group>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.CreateGroup(groupModel) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_SaveModel()
        {
            //Arrange
            GroupModel groupModel = new GroupModel()
            {
                GROUP_ID = 1,
                COURSE_ID = 1,
                NAME = "name"
            };

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Create(It.IsAny<Group>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            //Act
            var result = await controller.CreateGroup(groupModel) as RedirectToActionResult;

            //Assert
            mockGroupRepository.Verify(a => a.Create(It.IsAny<Group>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task EditGroupViewResultNotNull()
        {
            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Group(1, new Name("name")));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditGroup(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditGroupPostAction_ModelError()
        {
            string expected = "EditGroup";

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Group(1, new Name("name")));
            mockGroupRepository.Setup(m => m.Update(new Group(1, new Name("name"))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            GroupModel groupModel = new GroupModel();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);
            controller.ModelState.AddModelError("GROUP_ID", "GROUP_ID not setup!");

            var result = await controller.EditGroup(groupModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_RedirectToGroupsOfCourse()
        {
            string expected = "GroupsOfCourse";

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Group(1, new Name("name")));
            mockGroupRepository.Setup(m => m.Update(new Group(1, new Name("name"))));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            GroupModel groupModel = new GroupModel()
            {
                GROUP_ID = 1,
                COURSE_ID = 1,
                NAME = "name"
            };

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditGroup(groupModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_SaveModel()
        {
            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new Group(1, new Name("name")));
            mockGroupRepository.Setup(m => m.Update(It.IsAny<Group>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            GroupModel groupModel = new GroupModel()
            {
                GROUP_ID = 1,
                COURSE_ID = 1,
                NAME = "name"
            };

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.EditGroup(groupModel) as RedirectToActionResult;

            mockGroupRepository.Verify(a => a.Update(It.IsAny<Group>()));
            mockUnitToWork.Verify(a => a.Save());
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_RedirectToGroupsOfCourse()
        {
            string expected = "GroupsOfCourse";

            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.GetWhere(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group>() { new Group(1, new Name("name")) });
            mockGroupRepository.Setup(m => m.Delete(It.IsAny<Group>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteGroup(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_SaveModel()
        {
            var mockGroupRepository = new Mock<IRepository<Group>>();
            mockGroupRepository.Setup(m => m.GetWhere(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group>() { new Group(1, new Name("name")) });
            mockGroupRepository.Setup(m => m.Delete(It.IsAny<Group>()));

            var mockUnitToWork = new Mock<IUnitOfWork>();
            mockUnitToWork.Setup(p => p.Groups).Returns(mockGroupRepository.Object);

            var mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockUnitToWork.Object, mockLogger.Object);

            var result = await controller.DeleteGroup(1) as RedirectToActionResult;

            mockGroupRepository.Verify(a => a.Delete(It.IsAny<Group>()));
            mockUnitToWork.Verify(a => a.Save());
        }
    }
}
