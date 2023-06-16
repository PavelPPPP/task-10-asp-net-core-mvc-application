using Core.Repositoties;
using Core.Entities;

namespace Core.DataSource
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository<Course> Courses { get; }
        IGroupRepository<Group> Groups { get; }
        IStudentRepository<Student> Students { get; }

        Task Save();
    }
}
