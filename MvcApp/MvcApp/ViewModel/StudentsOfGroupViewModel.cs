using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class StudentsOfGroupViewModel
    {
        public IEnumerable<Group> Groups { get; }
        public IEnumerable<Student> Students { get; }
        public PageViewModel PageViewModel { get; }

        public StudentsOfGroupViewModel(IEnumerable<Group> groups, IEnumerable<Student> students, PageViewModel pageViewModel)
        {
            Groups = groups;
            Students = students;
            PageViewModel = pageViewModel;
        }
    }
}
