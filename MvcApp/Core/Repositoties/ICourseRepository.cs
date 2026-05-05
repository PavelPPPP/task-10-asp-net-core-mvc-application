using System.Linq.Expressions;

namespace Core.Repositoties
{
    public interface ICourseRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<U>> GetAllWithProjection<U>(Expression<Func<T, U>> selector);
        Task<T> GetWithDetail(int? courseId);
    }
}
