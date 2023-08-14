﻿
using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisCacheServerUrl"];
            });
            services.AddScoped<ApiHelper>();
            services.AddScoped<CacheRedisHelper>();
            services.AddScoped<FormatDataHelper>();
            services.AddScoped<LogHelper>();
            services.AddScoped<ReportHelper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IBGTSpeedOversService, BGTSpeedOversService>();
            services.AddScoped<IBGTTranportTypesService, BGTTranportTypesService>();
            services.AddScoped<IBGTVehicleTransportTypesService, BGTVehicleTransportTypesService>();
            services.AddScoped<IVehiclesService, VehiclesService>();
            services.AddScoped<IReportActivitySummariesService, ReportActivitySummariesService>();
            services.AddScoped<IReportVehicleSpeedViolationService, ReportVehicleSpeedViolationService>();
        }
    }
}
