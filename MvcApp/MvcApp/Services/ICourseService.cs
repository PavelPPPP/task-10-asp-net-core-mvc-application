using MvcApp.Models;

namespace MvcApp.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAllCourses();
        Task<CourseModel> Get(int? courseId);
        Task<CourseModel> GetOnlyName(int? courseId);
        Task CreateCourse(CourseModel course);
        Task UpdateCourse(CourseModel course);
        Task DeleteCourse(int? courseId);
    }
}
