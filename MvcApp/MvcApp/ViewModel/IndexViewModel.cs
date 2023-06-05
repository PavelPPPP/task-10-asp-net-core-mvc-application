using MvcApp.Models;

namespace MvcApp.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<CourseModel> Courses { get; }
        public PageViewModel PageViewModel { get; }
        public IndexViewModel(IEnumerable<CourseModel> courses, PageViewModel viewModel)
        {
            Courses = courses;
            PageViewModel = viewModel;
        }
    }
}
