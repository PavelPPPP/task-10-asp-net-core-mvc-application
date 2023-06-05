using Core.Repositoties;
using Core.Entities;

namespace Core.DataSource
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Course> Courses { get; }
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }

        Task Save();
    }
}
