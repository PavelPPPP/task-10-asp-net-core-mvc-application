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
            _services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            _services.AddScoped<ICourseService, CourseService>();
            _services.AddScoped<IGroupService, GroupService>();
            _services.AddScoped<IStudentService, StudentService>();
        }
    }
}
