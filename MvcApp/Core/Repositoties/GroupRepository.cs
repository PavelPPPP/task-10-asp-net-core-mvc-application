using Core.DataSource;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class GroupRepository : IGroupRepository<Group>
    {
        private readonly SchoolContext _dbContext;

        public GroupRepository(SchoolContext schoolContext)
        {
            _dbContext = schoolContext;
        }

        public async Task<IEnumerable<TResult>> GetGroupsOfCourse<TResult>(int? courseId, Expression<Func<Group, TResult>> selector)
        {
            if (courseId == null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var result = await _dbContext.Groups
                .Where(g => g.CourseId == courseId)
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<Group> Get(int? groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var group = await _dbContext.Groups
                .Where(g => g.Id == groupId)
                .SingleOrDefaultAsync();

            return group;
        }

        public async Task<TResult> GetWithProjection<TResult>(int? groupId, Expression<Func<Group, TResult>> selector)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var group = await _dbContext.Groups
                .Where(c => c.Id == groupId)
                .Select(selector)
                .SingleOrDefaultAsync();

            return group;
        }

        public async Task<Group> GetWithDetail(int? groupId)
        {
            if (groupId == null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var group = await _dbContext.Groups
                .Include(p => p.Students)
                .Where(c => c.Id == groupId)
                .SingleOrDefaultAsync();

            return group;
        }

        public async Task Create(Group group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            await _dbContext.Groups.AddAsync(group);
        }

        public void Update(Group group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            _dbContext.Groups.Update(group);
        }

        public void Delete(Group group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            _dbContext.Groups.Remove(group);
        }
    }
}
