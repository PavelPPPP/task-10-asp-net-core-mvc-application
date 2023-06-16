using Core.Entities;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int? id);
        Task<U> GetWithProjection<U>(int? id, Expression<Func<T, U>> selector);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
