using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;
using MvcApp.ViewModel;

namespace MvcApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IGroupService groupService, IStudentService studentService, ILogger<StudentController> logger)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> StudentsOfGroup(int? id, int page = 1)
        {
            _logger.LogDebug("Request show groups of course");

            GroupModel groupModel = await _groupService.GetOnlyIdAndName(id);
            IEnumerable<StudentModel> studentModels = await _studentService.GetStudentsOfGroup(id);

            int pageSize = 3;

            var count = studentModels.Count();
            var items = studentModels.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            StudentsOfGroupViewModel viewModel = new StudentsOfGroupViewModel(groupModel, items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult Create(int? groupId)
        {
            _logger.LogDebug("Request to show form create student");

            ViewData["groupId"] = groupId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentModel studentModel)
        {
            _logger.LogDebug("POST request to create student");

            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            await _studentService.CreateStudent(studentModel);

            return RedirectToAction("StudentsOfGroup", new { id = studentModel.GroupId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogDebug("Request to show form edit student");

            var studentModel = await _studentService.GetStudentModel(id);

            if (studentModel is null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel studentModel)
        {
            _logger.LogDebug("POST request to edit student");

            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            await _studentService.UpdateStudent(studentModel);
            return RedirectToAction("StudentsOfGroup", new { id = studentModel.GroupId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, int? groupId)
        {
            _logger.LogDebug("POST request to delete student");

            if (id is null)
            {
                return NotFound();
            }

            if (groupId is null)
            {
                return NotFound();
            }

            await _studentService.DeleteStudent(id);

            return RedirectToAction("StudentsOfGroup", "Student", new { id = groupId });
        }
    }
}
