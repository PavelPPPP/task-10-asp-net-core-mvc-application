using System.Linq.Expressions;

namespace Core.Repositoties
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T> Get(int? id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
