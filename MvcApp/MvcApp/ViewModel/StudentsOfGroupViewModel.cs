using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class StudentsOfGroupViewModel
    {
        public GroupModel Group { get; }
        public IEnumerable<StudentModel> Students { get; }
        public PageViewModel PageViewModel { get; }

        public StudentsOfGroupViewModel(GroupModel group, IEnumerable<StudentModel> students, PageViewModel pageViewModel)
        {
            Group = group;
            Students = students;
            PageViewModel = pageViewModel;
        }
    }
}
