using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class GroupsOfCourseViewModel
    {
        public CourseModel Course { get; }
        public IEnumerable<GroupModel> Groups { get; }
        public PageViewModel PageViewModel { get; }

        public GroupsOfCourseViewModel(CourseModel course, IEnumerable<GroupModel> groups, PageViewModel pageViewModel)
        {
            Course = course;
            Groups = groups;
            PageViewModel = pageViewModel;
        }
    }
}
