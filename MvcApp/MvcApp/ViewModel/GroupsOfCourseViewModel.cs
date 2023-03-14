using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class GroupsOfCourseViewModel
    {
        public IEnumerable<Course> Courses { get; }
        public IEnumerable<Group> Groups { get; }
        public PageViewModel PageViewModel { get; }

        public GroupsOfCourseViewModel(IEnumerable<Course> courses, IEnumerable<Group> groups, PageViewModel pageViewModel)
        {
            Courses = courses;
            Groups = groups;
            PageViewModel = pageViewModel;
        }
    }
}
