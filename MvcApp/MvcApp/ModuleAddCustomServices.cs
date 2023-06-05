using Core.DataSource;

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
        }
    }
}
