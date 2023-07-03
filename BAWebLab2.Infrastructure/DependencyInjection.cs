﻿using BAWebLab2.DAL.DataContext;
using BAWebLab2.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BAWebLab2.DAL
{
	public static class DependencyInjection
    {
        public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BADbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
