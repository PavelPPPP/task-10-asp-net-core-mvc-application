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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _courseRepository = new CourseRepository(dbContext);
            _groupRepository = new GroupRepository(dbContext);
            _studentRepository = new StudentRepository(dbContext);
        }

        public ICourseRepository<Course> Courses => _courseRepository;

        public IGroupRepository<Group> Groups => _groupRepository;

        public IStudentRepository<Student> Students => _studentRepository; 


        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
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
