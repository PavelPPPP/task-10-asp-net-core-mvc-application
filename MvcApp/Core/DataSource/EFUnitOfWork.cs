using Core.Repositoties;
using Core.Entities;

namespace Core.DataSource
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SchoolContext _dbContext;
        private CourseRepository _courseRepository;
        private GroupRepository _groupRepository;
        private StudentRepository _studentRepository;
        private bool _disposed = false;

        public EFUnitOfWork(SchoolContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public IRepository<Course> Courses
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_dbContext);
                }

                return _courseRepository;
            }
        }

        public IRepository<Group> Groups
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_dbContext);
                }

                return _groupRepository;
            }
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_dbContext);
                }

                return _studentRepository;
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
