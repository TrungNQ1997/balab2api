using BAWebLab2.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BAWebLab2.Core
{
	public static class DependencyInjection
    {
        public static void RegisterCoreDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
              services.AddScoped<IUserService, UserServiceImpl>();
                 }
    }
}
