using System.Linq.Expressions;

namespace Core.Repositoties
{
    public interface IStudentRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<U>> GetStudentsOfGroup<U>(int? groupId, Expression<Func<T, U>> selector);
    }
}
