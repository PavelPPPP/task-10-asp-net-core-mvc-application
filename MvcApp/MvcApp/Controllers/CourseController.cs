using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;
using MvcApp.ViewModel;

namespace MvcApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            _logger.LogDebug("Request show courses");

            IEnumerable<CourseModel> courses = await _courseService.GetAllCourses();

            int pageSize = 3;

            var count = courses.Count();
            var items = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            _logger.LogDebug("Request to create course");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseModel courseModel)
        {
            _logger.LogDebug("Request to create course");

            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            await _courseService.CreateCourse(courseModel);
            return RedirectToAction("");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogDebug("Request to view edit course");

            var courseModel = await _courseService.Get(id);

            if (courseModel is null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseModel courseModel)
        {
            _logger.LogDebug("POST Request to edit course");

            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            await _courseService.UpdateCourse(courseModel);
            return RedirectToAction("");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogDebug("Request to delete course");

            if (id is null)
            {
                return NotFound();
            }

            await _courseService.DeleteCourse(id);
            return RedirectToAction("");
        }
    }
}
