using Core.Entities;
using Core.DataSource;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class StudentRepository : IStudentRepository<Student>
    {
        private readonly SchoolContext _dbContext;

        public StudentRepository(SchoolContext schoolContext)
        {
            _dbContext = schoolContext;
        }

        public async Task<IEnumerable<TResult>> GetStudentsOfGroup<TResult>(int? groupId, Expression<Func<Student, TResult>> selector)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var result = await _dbContext.Students
                .Where(s => s.GroupId == groupId)
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<Student> Get(int? studentId)
        {
            if (studentId == null)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            var student = await _dbContext.Students
                .Where(g => g.Id == studentId)
                .SingleOrDefaultAsync();

            return student;
        }

        public async Task<TResult> GetWithProjection<TResult>(int? studentId, Expression<Func<Student, TResult>> selector)
        {
            if (studentId == null)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var student = await _dbContext.Students
                .Where(s => s.Id == studentId)
                .Select(selector)
                .SingleOrDefaultAsync();

            return student;
        }

        public async Task Create(Student student)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            await _dbContext.Students.AddAsync(student);
        }

        public void Update(Student student)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            _dbContext.Students.Update(student);
        }

        public void Delete(Student student)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            _dbContext.Students.Remove(student);
        }
    }
}
