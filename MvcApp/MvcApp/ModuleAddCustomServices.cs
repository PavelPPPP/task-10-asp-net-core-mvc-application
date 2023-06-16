using Core.DataSource;
using MvcApp.Services;

namespace MvcApp
{
    public class ModuleAddCustomServices
    {
        private IServiceCollection _services;
        public ModuleAddCustomServices(IServiceCollection services)
        {
            _services = services;
        }

        public void Load()
        {
            _services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            _services.AddTransient<ICourseService, CourseService>();
            _services.AddTransient<IGroupService, GroupService>();
            _services.AddTransient<IStudentService, StudentService>();
        }
    }
}
