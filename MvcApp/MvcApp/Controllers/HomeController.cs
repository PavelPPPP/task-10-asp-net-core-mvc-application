using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
using MvcApp.ViewModel;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        SchoolContext db;

        public HomeController(SchoolContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Course> courses = await db.Courses.ToListAsync();
            int pageSize = 3;

            var count = courses.Count();
            var items = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id != null)
            {
                Course? course = await db.Courses.FirstOrDefaultAsync(s => s.COURSE_ID == id);
                if (course != null)
                {
                    return View(course);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(Course course)
        {
            db.Courses.Update(course);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id != null)
            {
                Course? course = await db.Courses.FirstOrDefaultAsync(s => s.COURSE_ID == id && s.Groups.Count == 0);

                if (course != null)
                {
                    db.Courses.Remove(course);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> GroupsOfCourse(int id, int page = 1)
        {
            IEnumerable<Course> courses = await db.Courses.Where(o => o.COURSE_ID == id).ToListAsync();
            IEnumerable<Group> groups = await db.Groups.Where(o => o.COURSE_ID == id).ToListAsync();

            int pageSize = 3;

            var count = groups.Count();
            var items = groups.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            GroupsOfCourseViewModel viewModel = new GroupsOfCourseViewModel(courses, items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult CreateGroup(int? courseId)
        {
            ViewData["courseId"] = courseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(Group group)
        {
            int groupCourseId = group.COURSE_ID;
            db.Groups.Add(group);
            await db.SaveChangesAsync();
            return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
        }

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id != null)
            {
                Group? group = await db.Groups.FirstOrDefaultAsync(s => s.GROUP_ID == id);
                if (group != null)
                {
                    return View(group);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(Group group)
        {
            int groupCourseId = group.COURSE_ID;
            db.Groups.Update(group);
            await db.SaveChangesAsync();
            return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id != null)
            {
                Group? group = await db.Groups.FirstOrDefaultAsync(s => s.GROUP_ID == id && s.Students.Count == 0);
                int? groupCourseId = group?.COURSE_ID;

                if (group != null)
                {
                    db.Groups.Remove(group);
                    await db.SaveChangesAsync();
                }
                else
                {
                    group = await db.Groups.FirstOrDefaultAsync(s => s.GROUP_ID == id);
                    groupCourseId = group?.COURSE_ID;
                }

                return RedirectToAction("GroupsOfCourse", new { id = groupCourseId });
            }

            return NotFound();
        }

        public async Task<IActionResult> StudentsOfGroup(int id, int page = 1)
        {
            IEnumerable<Group> groups = await db.Groups.Where(o => o.GROUP_ID == id).ToListAsync();
            IEnumerable<Student> students = await db.Students.Where(o => o.GROUP_ID == id).ToListAsync();

            int pageSize = 3;

            var count = students.Count();
            var items = students.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            StudentsOfGroupViewModel viewModel = new StudentsOfGroupViewModel(groups, items, pageViewModel);

            return View(viewModel);
        }

        public IActionResult CreateStudent(int? groupId)
        {
            ViewData["groupId"] = groupId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            int studentGroupId = student.GROUP_ID;
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
        }

        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id != null)
            {
                Student? student = await db.Students.FirstOrDefaultAsync(s => s.STUDENT_ID == id);
                if (student != null)
                {
                    return View(student);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student student)
        {
            int studentGroupId = student.GROUP_ID;
            db.Students.Update(student);
            await db.SaveChangesAsync();
            return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id != null)
            {
                Student? student = await db.Students.FirstOrDefaultAsync(s => s.STUDENT_ID == id);
                int? studentGroupId = student?.GROUP_ID;

                if (student != null)
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("StudentsOfGroup", new { id = studentGroupId });
                }
            }

            return NotFound();
        }
    }
}
