using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories;
using BAWebLab2.Infrastructure.Repositories.IRepository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BAWebLab2.Infrastructure
{
    /// <summary>class khai báo đăng kí phụ thuộc kết nối sql, các lớp implement-interface</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public static class DependencyInjection
    {
        /// <summary>khai báo đăng kí phụ thuộc kết nối sql, các lớp implement-interface</summary>
        /// <param name="services">services</param>
        /// <param name="Configuration">config ở file appsettings.json .</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BADbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                
            });

            //using (var context = services.GetService<AppDbContext>())
            //{
            //	var product = new Product
            //	{
            //		Name = "Sample Product"
            //		// Set other properties here
            //	};

            //	context.Products.Add(product); // Thêm bản ghi vào DbSet<Product>
            //	context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            //}
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
            services.AddScoped<IBGTVehicleTransportTypesRepository, BGTVehicleTransportTypesRepository>();
            services.AddScoped<IBGTTranportTypesRepository, BGTTranportTypesRepository>();
            services.AddScoped<IBGTSpeedOversRepository, BGTSpeedOversRepository>();
            services.AddScoped<IReportActivitySummariesRepository, ReportActivitySummariesRepository>();
             
        }
    }
}
