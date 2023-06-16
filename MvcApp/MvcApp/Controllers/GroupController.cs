using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;
using MvcApp.ViewModel;

namespace MvcApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(ICourseService courseService, IGroupService groupService, ILogger<GroupController> logger)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IActionResult> GroupsOfCourse(int? id, int page = 1)
        {
            _logger.LogDebug("Request show groups of course");

            CourseModel course = await _courseService.GetOnlyName(id);
            IEnumerable<GroupModel> groups = await _groupService.GetGroupsOfCourse(id);

            int pageSize = 3;

            var count = groups.Count();
            var items = groups.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            GroupsOfCourseViewModel viewModel = new GroupsOfCourseViewModel(course, items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult Create(int? courseId)
        {
            _logger.LogDebug("Request to show form create group");

            ViewData["courseId"] = courseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupModel groupModel)
        {
            _logger.LogDebug("Request to create group");

            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            await _groupService.CreateGroup(groupModel);

            return RedirectToAction("GroupsOfCourse", new { id = groupModel.CourseId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogDebug("Request to show edit group");

            var groupModel = await _groupService.GetGroupModel(id);

            if (groupModel is null)
            {
                return NotFound();
            }

            return View(groupModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupModel groupModel)
        {
            _logger.LogDebug("POST Request to edit group");

            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            await _groupService.UpdateGroup(groupModel);
            return RedirectToAction("GroupsOfCourse", "Group", new { id = groupModel.CourseId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, int? courseId)
        {
            _logger.LogDebug("POST Request to delete group");

            if (id is null)
            {
                return NotFound();
            }

            if (courseId is null)
            {
                return NotFound();
            }
            
            await _groupService.DeleteGroup(id);
            return RedirectToAction("GroupsOfCourse", "Group", new { id = courseId });
        }
    }
}
