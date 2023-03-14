using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Course> Courses { get; }
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<Course> courses, PageViewModel viewModel)
        {
            Courses = courses;
            PageViewModel = viewModel;
        }
    }
}
