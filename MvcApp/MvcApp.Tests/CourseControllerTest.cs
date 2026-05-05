using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using MvcApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Tests
{
    [TestClass]
    public class CourseControllerTest
    {
        private Mock<ICourseService> mockCourseService;
        private Mock<ILogger<CourseController>> mockLogger;
        private CourseController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            mockCourseService = new Mock<ICourseService>();
            mockLogger = new Mock<ILogger<CourseController>>();

            controller = new CourseController(mockCourseService.Object, mockLogger.Object);
        }

        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CourseController(null, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new CourseController(mockCourseService.Object, null));
        }

        [TestMethod]
        public async Task IndexViewResultNotNull()
        {
            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_ModelError()
        {
            string expected = "Create";

            CourseModel courseModel = new CourseModel();

            controller.ModelState.AddModelError("CourseID", "CourseID not setup!");

            ViewResult result = await controller.Create(courseModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_RedirectToIndexView()
        {
            string expected = "";

            CourseModel courseModel = new CourseModel();

            var result = await controller.Create(courseModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateCoursePostAction_SaveModel()
        {
            CourseModel courseModel = new CourseModel();

            var result = await controller.Create(courseModel) as RedirectToActionResult;

            mockCourseService.Verify(a => a.CreateCourse(It.IsAny<CourseModel>()));
        }

        [TestMethod]
        public async Task EditCourseViewResultNotNull()
        {
            mockCourseService.Setup(m => m.Get(It.IsAny<int?>())).ReturnsAsync(new CourseModel());

            var result = await controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditCoursePostAction_ModelError()
        {
            string expected = "Edit";

            CourseModel courseModel = new CourseModel();

            controller.ModelState.AddModelError("CourseID", "CourseID not setup!");

            var result = await controller.Edit(courseModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditCoursePostAction_RedirectToIndexView()
        {
            string expected = "";

            CourseModel courseModel = new CourseModel();

            var result = await controller.Edit(courseModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditCoursePostAction_SaveModel()
        {
            CourseModel courseModel = new CourseModel();

            var result = await controller.Edit(courseModel) as RedirectToActionResult;

            mockCourseService.Verify(m => m.UpdateCourse(It.IsAny<CourseModel>()));
        }

        [TestMethod]
        public async Task DeleteCoursePostAction_RedirectToIndexView()
        {
            string expected = "";

            var result = await controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteCoursePostAction_SaveModel()
        {
            var result = await controller.Delete(1) as RedirectToActionResult;

            mockCourseService.Verify(m => m.DeleteCourse(It.IsAny<int?>()));
        }
    }
}