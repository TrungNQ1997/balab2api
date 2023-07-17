
using BAWebLab2.Core.Services;
using BAWebLab2.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BAWebLab2.Core
{
    /// <summary>class khai báo các đăng kí phụ thuộc</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public static class DependencyInjection
    {
        /// <summary>hàm khai báo các đăng kí phụ thuộc, các class implement, interface</summary>
        /// <param name="services">The services.</param>
        /// <param name="Configuration">config từ file appsettings.json</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public static void RegisterCoreDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
