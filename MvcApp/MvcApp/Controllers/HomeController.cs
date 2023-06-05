using Core.DataSource;
using Core.Entities;
using Core.Repositoties;
using Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.ViewModel;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _courseRepository = unitOfWork.Courses;
            _groupRepository = unitOfWork.Groups;
            _studentRepository = unitOfWork.Students;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            _logger.LogDebug("Request show courses");

            IEnumerable<Course> courses = await _courseRepository.GetAll();
            var coursesModel = courses.Select((o) => {
                return new CourseModel()
                {
                    COURSE_ID = o.Id,
                    NAME = o.DataCourse.Name,
                    DESCRIPTION = o.DataCourse.Description
                };
            });
            
            int pageSize = 3;

            var count = coursesModel.Count();
            var items = coursesModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult CreateCourse()
        {
            _logger.LogDebug("Show form for create course");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseModel courseModel)
        {
            _logger.LogDebug("Request to create course");

            if (!ModelState.IsValid)
            {
                return View("CreateCourse");
            }

            var course = new Course(
                new DataCourse(courseModel.NAME, courseModel.DESCRIPTION));
            await _courseRepository.Create(course);
            await _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id != null)
            {
                Course course = await _courseRepository.Get(id);

                if (course != null)
                {
                    CourseModel courseModel = new CourseModel()
                    {
                        COURSE_ID = course.Id,
                        NAME = course.DataCourse.Name,
                        DESCRIPTION = course.DataCourse.Description
                    };

                    return View(courseModel);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseModel courseModel)
        {
            _logger.LogDebug("Request to edit course");

            if (!ModelState.IsValid)
            {
                return View("EditCourse");
            }

            var course = await _courseRepository.Get(courseModel.COURSE_ID);
            course.Change(new DataCourse(courseModel.NAME, courseModel.DESCRIPTION));

            _courseRepository.Update(course);
            await _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            _logger.LogDebug("Request to delete course");

            if (id != null)
            {
                var courses = await _courseRepository.GetWhere(c => c.Id == id && c.Groups.Count == 0);

                if (courses.Count() != 0)
                {
                    Course course = courses.First();

                    _courseRepository.Delete(course);
                    await _unitOfWork.Save();
                    
                }

                return RedirectToAction("Index");
            }

            return NotFound();
        }
        
        public async Task<IActionResult> GroupsOfCourse(int? id, int page = 1)
        {
            Course course = await _courseRepository.Get(id);
            CourseModel courseModel = new CourseModel()
            {
                COURSE_ID = course.Id,
                NAME = course.DataCourse.Name,
                DESCRIPTION = course.DataCourse.Description
            };

            IEnumerable<Group> groups = course.Groups;
            var groupsModel = groups.Select((g) =>
            {
                return new GroupModel()
                {
                    GROUP_ID = g.Id,
                    COURSE_ID = g.CourseId,
                    NAME = g.Name.Value
                };
            });

            int pageSize = 3;

            var count = groupsModel.Count();
            var items = groupsModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            GroupsOfCourseViewModel viewModel = new GroupsOfCourseViewModel(courseModel, items, pageViewModel);

            return View(viewModel);
        }
        
        public IActionResult CreateGroup(int? courseId)
        {
            ViewData["courseId"] = courseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(GroupModel groupModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateGroup");
            }

            int? groupCourseId = groupModel.COURSE_ID;
            Group group = new Group(groupCourseId, new Name(groupModel.NAME));
            await _groupRepository.Create(group);
            await _unitOfWork.Save();
            return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
        }
        
        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id != null)
            {
                Group? group = await _groupRepository.Get(id);
                if (group != null)
                {
                    GroupModel groupModel = new GroupModel()
                    {
                        GROUP_ID = group.Id,
                        COURSE_ID = group.CourseId,
                        NAME = group.Name.Value
                    };
                    return View(groupModel);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(GroupModel groupModel)
        {
            _logger.LogDebug("Request to edit group");

            if (!ModelState.IsValid)
            {
                return View("EditGroup");
            }

            int? groupCourseId = groupModel.COURSE_ID;
            Group group = await _groupRepository.Get(groupModel.GROUP_ID);
            group.Change(groupCourseId, new Name(groupModel.NAME));
            _groupRepository.Update(group);
            await _unitOfWork.Save();
            return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id != null)
            {
                int? groupCourseId;
                var groups = await _groupRepository.GetWhere(g => g.Id == id && g.Students.Count == 0);
                if (groups.Count() != 0)
                {
                    Group? group = groups.First();

                    groupCourseId = group?.CourseId;

                    _groupRepository.Delete(group);
                    await _unitOfWork.Save();
                }
                else
                {
                    Group group = await _groupRepository.Get(id);
                    groupCourseId = group?.CourseId;
                }

                return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
            }

            return NotFound();
        }
        
        public async Task<IActionResult> StudentsOfGroup(int id, int page = 1)
        {
            Group group = await _groupRepository.Get(id);
            var groupModel = new GroupModel()
            {
                GROUP_ID = group.Id,
                COURSE_ID = group.CourseId,
                NAME = group.Name.Value
            };

            IEnumerable<Student> students = group.Students;
            var studentsModel = students.Select((s) =>
            {
                return new StudentModel()
                {
                    STUDENT_ID = s.Id,
                    GROUP_ID = s.GroupId,
                    FIRST_NAME = s.StudentName.FirstName.Value,
                    LAST_NAME = s.StudentName.LastName.Value
                };
            });

            int pageSize = 3;

            var count = studentsModel.Count();
            var items = studentsModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            StudentsOfGroupViewModel viewModel = new StudentsOfGroupViewModel(groupModel, items, pageViewModel);

            return View(viewModel);
        }
        
        public IActionResult CreateStudent(int? groupId)
        {
            ViewData["groupId"] = groupId;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentModel studentModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateStudent");
            }

            int? studentGroupId = studentModel.GROUP_ID;
            StudentName studentName = new StudentName(new Name(studentModel.FIRST_NAME), new Name(studentModel.LAST_NAME));            
            Student student = new Student(studentGroupId, studentName);
            await _studentRepository.Create(student);
            await _unitOfWork.Save();
            return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
        }
        
        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id != null)
            {
                Student? student = await _studentRepository.Get(id);
                if (student != null)
                {
                    StudentModel studentModel = new StudentModel()
                    {
                        STUDENT_ID = student.Id,
                        GROUP_ID = student.GroupId,
                        FIRST_NAME = student.StudentName.FirstName.Value,
                        LAST_NAME = student.StudentName.LastName.Value
                    };

                    return View(studentModel);
                }
            }

            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentModel studentModel)
        {
            _logger.LogDebug("Request to edit student");

            if (!ModelState.IsValid)
            {
                return View("EditStudent");
            }

            int? studentGroupId = studentModel.GROUP_ID;
            Student student = await _studentRepository.Get(studentModel.STUDENT_ID);
            student.Change(studentGroupId, new StudentName(new Name(studentModel.FIRST_NAME), new Name(studentModel.LAST_NAME)));
            _studentRepository.Update(student);
            await _unitOfWork.Save();
            return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id != null)
            {
                Student? student = await _studentRepository.Get(id);
                int? studentGroupId = student?.GroupId;

                if (student != null)
                {
                    _studentRepository.Delete(student);
                    await _unitOfWork.Save();
                    return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
                }
            }

            return NotFound();
        }
    }
}
