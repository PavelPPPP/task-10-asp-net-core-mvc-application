using Core.Entities;
using Core.DataSource;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class CourseRepository : ICourseRepository<Course>
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

        public async Task<IEnumerable<TResult>> GetAllWithProjection<TResult>(Expression<Func<Course, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var result = await _dbContext.Courses
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<Course> Get(int? courseId)
        {
            if (courseId == null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var course = await _dbContext.Courses
                .Where(c => c.Id == courseId)
                .SingleOrDefaultAsync();

            return course;
        }

        public async Task<TResult> GetWithProjection<TResult>(int? courseId, Expression<Func<Course, TResult>> selector)
        {
            if (courseId == null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            
            var course = await _dbContext.Courses
                .Where(c => c.Id == courseId)
                .Select(selector)
                .SingleOrDefaultAsync();
            
            return course;
        }

        public async Task<Course> GetWithDetail(int? courseId)
        {
            if (courseId == null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var course = await _dbContext.Courses
                .Include(p => p.Groups)
                .Where(c => c.Id == courseId)
                .SingleOrDefaultAsync();

            return course;
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
