using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using MvcApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Tests
{
    [TestClass]
    public class StudentControllerTest
    {
        private Mock<IStudentService> mockStudentService;
        private Mock<IGroupService> mockGroupService;
        private Mock<ILogger<StudentController>> mockLogger;
        private StudentController studentController;

        [TestInitialize]
        public void TestInitialize()
        {
            mockStudentService = new Mock<IStudentService>();
            mockGroupService = new Mock<IGroupService>();
            mockLogger = new Mock<ILogger<StudentController>>();

            studentController = new StudentController(mockGroupService.Object, mockStudentService.Object, mockLogger.Object);
        }

        [TestMethod]
        public void WhenConstructorParamIsNull_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new StudentController(null, mockStudentService.Object, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentController(mockGroupService.Object, null, mockLogger.Object));
            Assert.ThrowsException<ArgumentNullException>(() => new StudentController(mockGroupService.Object, mockStudentService.Object, null));
        }

        [TestMethod]
        public async Task StudentsOfGroupViewResultNotNull()
        {
            var result = await studentController.StudentsOfGroup(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task CreateStudentPostAction_ModelError()
        {
            string expected = "Create";

            StudentModel studentModel = new StudentModel();

            studentController.ModelState.AddModelError("StudentId", "StudentId not setup");

            ViewResult result = await studentController.Create(studentModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task CreateStudentPostAction_RedirectToIndexView()
        {
            string expected = "StudentsOfGroup";

            StudentModel studentModel = new StudentModel();

            var result = await studentController.Create(studentModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task CreateStudentPostAction_SaveModel()
        {
            StudentModel studentModel = new StudentModel();

            var result = await studentController.Create(studentModel) as RedirectToActionResult;

            mockStudentService.Verify(a => a.CreateStudent(It.IsAny<StudentModel>()));
        }

        [TestMethod]
        public async Task EditStudentViewResultNotNull()
        {
            StudentModel studentModel = new StudentModel();

            mockStudentService.Setup(m => m.GetStudentModel(It.IsAny<int?>())).ReturnsAsync(new StudentModel());

            var result = await studentController.Edit(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public async Task EditStudentPostAction_ModelError()
        {
            string expected = "Edit";

            StudentModel studentModel = new StudentModel();

            studentController.ModelState.AddModelError("StudentId", "StudentId not setup");

            var result = await studentController.Edit(studentModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public async Task EditStudentPostAction_RedirectToGroupsOfCourseView()
        {
            string expected = "StudentsOfGroup";

            StudentModel studentModel = new StudentModel();

            var result = await studentController.Edit(studentModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task EditGroupPostAction_SaveModel()
        {
            StudentModel studentModel = new StudentModel();

            var result = await studentController.Edit(studentModel) as RedirectToActionResult;

            mockStudentService.Verify(a => a.UpdateStudent(It.IsAny<StudentModel>()));
        }

        [TestMethod]
        public async Task DeleteStudentPostAction_RedirectToIndexView()
        {
            string expected = "StudentsOfGroup";

            var result = await studentController.Delete(1, 1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ActionName);
        }

        [TestMethod]
        public async Task DeleteStudentPostAction_SaveModel()
        {
            var result = await studentController.Delete(1, 1) as RedirectToActionResult;

            mockStudentService.Verify(m => m.DeleteStudent(It.IsAny<int?>()));
        }
    }
}
