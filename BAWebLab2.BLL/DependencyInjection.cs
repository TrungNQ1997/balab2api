using BAWebLab2.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.BLL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IUserBusiness, UserBusinessImpl>();
            //services.AddScoped<IAuthService, AuthService>();

            //services.AddFluentValidationAutoValidation();
            //services.AddValidatorsFromAssemblyContaining<UserToLoginDTOValidator>();
            //services.AddValidatorsFromAssemblyContaining<UserToRegisterDTOValidator>();
        }
    }
}
