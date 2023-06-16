using System.Linq.Expressions;

namespace Core.Repositoties
{
    public interface IGroupRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<U>> GetGroupsOfCourse<U>(int? courseId, Expression<Func<T, U>> selector);
        Task<T> GetWithDetail(int? id);
    }
}
