using Core.Entities;
using Core.DataSource;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly SchoolContext _dbContext;

        public CourseRepository(SchoolContext schoolContext)
        {
            if (schoolContext == null)
            {
                throw new ArgumentNullException(nameof(schoolContext));
            }

            _dbContext = schoolContext;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetWhere(Expression<Func<Course, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await _dbContext.Courses.Where(predicate).ToListAsync();
        }

        public async Task<Course> Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var courses = await _dbContext.Courses.Include(p => p.Groups).Where(c => c.Id == id).ToListAsync();

            if (courses == null)
            {
                throw new Exception("Result must not be null");
            }
            
            return courses.First();
        }

        public async Task Create(Course course)
        {
            if (course is null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            await _dbContext.Courses.AddAsync(course);
        }

        public void Update(Course course)
        {
            if (course is null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            _dbContext.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            if (course is null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            _dbContext.Courses.Remove(course);
        }
    }
}
