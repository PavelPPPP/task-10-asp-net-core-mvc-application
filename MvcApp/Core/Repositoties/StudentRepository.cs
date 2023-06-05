using Core.Entities;
using Core.DataSource;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly SchoolContext _dbContext;

        public StudentRepository(SchoolContext schoolContext)
        {
            _dbContext = schoolContext;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _dbContext.Students.Include(p => p.GroupId).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetWhere(Expression<Func<Student, bool>> predicate)
        {
            return await _dbContext.Students.Include(p => p.Group).Where(predicate).ToListAsync();
        }

        public async Task<Student> Get(int? id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task Create(Student student)
        {
            await _dbContext.Students.AddAsync(student);
        }

        public void Update(Student student)
        {
            _dbContext.Students.Update(student);
        }

        public void Delete(Student student)
        {
            if (student != null)
                _dbContext.Students.Remove(student);
        }
    }
}
