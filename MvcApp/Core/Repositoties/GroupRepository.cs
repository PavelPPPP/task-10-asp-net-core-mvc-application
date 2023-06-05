using Core.DataSource;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositoties
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly SchoolContext _dbContext;

        public GroupRepository(SchoolContext schoolContext)
        {
            _dbContext = schoolContext;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _dbContext.Groups.Include(p => p.CourseId).ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetWhere(Expression<Func<Group, bool>> predicate)
        {
            return await _dbContext.Groups.Include(p => p.Students).Where(predicate).ToListAsync();
        }

        public async Task<Group> Get(int? id)
        {
            var groups = await _dbContext.Groups.Include(p => p.Students).Where(c => c.Id == id).ToListAsync();

            return groups.First();
        }

        public async Task Create(Group group)
        {
            await _dbContext.Groups.AddAsync(group);
        }

        public void Update(Group group)
        {
            _dbContext.Groups.Update(group);
        }

        public void Delete(Group group)
        {
            if (group != null)
                _dbContext.Groups.Remove(group);
        }
    }
}
