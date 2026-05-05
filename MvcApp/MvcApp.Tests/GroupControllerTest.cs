using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using MvcApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Tests
{
    [TestClass]
    public class GroupControllerTest
    {
        private Mock<ICourseService> mockCourseService;
        private Mock<IGroupService> mockGroupService;
        private Mock<ILogger<GroupController>> mockLogger;
        GroupController groupController;

        [TestInitialize]
        public void TestInitialize()
        {
            mockCourseService = new Mock<ICourseService>();
            mockGroupService = new Mock<IGroupService>();
            mockLogger = new Mock<ILogger<GroupController>>();

            groupController = new GroupController(mockCourseService.Object, mockGroupService.Object, mockLogger.Object);
        }

        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GroupController(null, mockGroupService.Object, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new GroupController(mockCourseService.Object, null, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new GroupController(mockCourseService.Object, mockGroupService.Object, null));
        }

        [TestMethod]
        public async Task GoupsOfCourseViewResultNotNull()
        {
            var result = await groupController.GroupsOfCourse(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_ModelError()
        {
            string expected = "Create";

            GroupModel groupModel = new GroupModel();

            groupController.ModelState.AddModelError("GroupId", "GroupId not setup");

            ViewResult result = await groupController.Create(groupModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_RedirectToIndexView()
        {
            string expected = "GroupsOfCourse";

            GroupModel groupModel = new GroupModel();

            var result = await groupController.Create(groupModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateGroupPostAction_SaveModel()
        {
            GroupModel groupModel = new GroupModel();

            var result = await groupController.Create(groupModel) as RedirectToActionResult;

            mockGroupService.Verify(a => a.CreateGroup(It.IsAny<GroupModel>()));
        }

        [TestMethod]
        public async Task EditGroupViewResultNotNull()
        {
            mockGroupService.Setup(m => m.GetGroupModel(It.IsAny<int?>())).ReturnsAsync(new GroupModel());

            var result = await groupController.Edit(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditGroupPostAction_ModelError()
        {
            string expected = "Edit";

            groupController.ModelState.AddModelError("GroupId", "GroupId not setup");

            GroupModel groupModel = new GroupModel();

            var result = await groupController.Edit(groupModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_RedirectToGroupsOfCourseView()
        {
            string expected = "GroupsOfCourse";

            GroupModel groupModel = new GroupModel();

            var result = await groupController.Edit(groupModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_SaveModel()
        {
            GroupModel groupModel = new GroupModel();

            var result = await groupController.Edit(groupModel) as RedirectToActionResult;

            mockGroupService.Verify(a => a.UpdateGroup(It.IsAny<GroupModel>()));
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_RedirectToIndexView()
        {
            string expected = "GroupsOfCourse";

            var result = await groupController.Delete(1, 1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteGroupPostAction_SaveModel()
        {
            var result = await groupController.Delete(1, 1) as RedirectToActionResult;

            mockGroupService.Verify(m => m.DeleteGroup(It.IsAny<int?>()));
        }
    }
}
