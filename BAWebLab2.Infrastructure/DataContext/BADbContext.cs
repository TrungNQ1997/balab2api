using BAWebLab2.Model;
using Microsoft.EntityFrameworkCore;
using BAWebLab2.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace BAWebLab2.Infrastructure.DataContext
{

    /// <summary>class kết nối db</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class BADbContext : DbContext
    {
        //private readonly string _redisConfiguration;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(10)
        };
        public BADbContext(DbContextOptions<BADbContext> options, IDistributedCache cache) : base(options)
        {

            _cache = cache; 
        }

        //public DbSet<User> Users { get; set; }
        /// <summary>dbset chứa vehicles từ db</summary>
        /// <value>The vehicles.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public DbSet<Vehicles> Vehicles { get; set; }

        /// <summary>dbset chứa danh sách BGTspeedovers lấy từ db</summary>
        /// <value>BGTspeedovers.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public DbSet<BGTSpeedOvers> BGTSpeedOvers { get; set; }

        /// <summary>dbset chứa danh sách BGTtranporttypes lấy từ db</summary>
        /// <value>BGTtranporttypes.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public DbSet<BGTTranportTypes> BGTTranportTypes { get; set; }

        /// <summary>dbset chứa BGTvehicletransporttypes lấy từ db</summary>
        /// <value>BGTvehicletransporttypes.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public DbSet<BGTVehicleTransportTypes> BGTVehicleTransportTypes { get; set; }

        /// <summary>dbset chứa danh sách report.activitysummaries lấy từ db</summary>
        /// <value>report.activitysummaries.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public DbSet<ReportActivitySummaries> ReportActivitySummaries { get; set; }

        /// <summary>lấy dữ liệu bảng BGTSpeedOvers có companyid = 15076, nếu có cache thì lấy cache</summary>
        /// <returns>list BGTSpeedOvers</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public IEnumerable<BGTSpeedOvers> GetAllBGTSpeedOvers()
        {
            var cachedData = _cache.Get("BGTSpeedOvers");
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonConvert.DeserializeObject<List<BGTSpeedOvers>>(cachedDataString);
            }
            else
            {
                var cachedDataString = JsonConvert.SerializeObject(BGTSpeedOvers.Where(m => m.FK_CompanyID == 15076));
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set("BGTSpeedOvers", newDataToCache);
                return BGTSpeedOvers.Where(m => m.FK_CompanyID == 15076);
            }

        }

        /// <summary>lấy dữ liệu bảng Vehicle.Vehicles có companyid = 15076, nếu có cache thì lấy cache</summary>
        /// <returns>danh sách Vehicles</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public IEnumerable<Vehicles> GetAllVehicles()
        {

            var cachedData = _cache.Get("Vehicles");
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonConvert.DeserializeObject<List<Vehicles>>(cachedDataString);
            }
            else
            {
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };
                var list = Vehicles.Where(m => m.FK_CompanyID == 15076);
                var cachedDataString = JsonConvert.SerializeObject(list);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set("Vehicles", newDataToCache, cacheOptions);
                return list;
            }

            //return Vehicles;
        }

        /// <summary>lấy dữ liệu bảng BGT.TranportTypes có companyid = 15076, nếu có cache thì lấy cache</summary>
        /// <returns>danh sách BGTTranportTypes</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public IEnumerable<BGTTranportTypes> GetAllBGTTranportTypes()
        {
            var cachedData = _cache.Get("BGTTranportTypes");
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonConvert.DeserializeObject<List<BGTTranportTypes>>(cachedDataString);
            }
            else
            {
                var cachedDataString = JsonConvert.SerializeObject(BGTTranportTypes);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set("BGTTranportTypes", newDataToCache);
                return BGTTranportTypes;
            }

            //return BGTTranportTypes;
        }

        /// <summary>lấy dữ liệu bảng BGT.VehicleTransportTypes có companyid = 15076, nếu có cache thì lấy cache</summary>
        /// <returns>danh sách BGTVehicleTransportTypes</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public IEnumerable<BGTVehicleTransportTypes> GetAllBGTVehicleTransportTypes()
        {
            var cachedData = _cache.Get("BGTVehicleTransportTypes");
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonConvert.DeserializeObject<List<BGTVehicleTransportTypes>>(cachedDataString);
            }
            else
            {
                var cachedDataString = JsonConvert.SerializeObject(BGTVehicleTransportTypes.Where(m => m.FK_CompanyID == 15076));
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set("BGTVehicleTransportTypes", newDataToCache);
                return BGTVehicleTransportTypes.Where(m => m.FK_CompanyID == 15076);
            }
            //return BGTVehicleTransportTypes;
        }

        /// <summary>lấy dữ liệu bảng Report.ActivitySummaries có companyid = 15076, nếu có cache thì lấy cache</summary>
        /// <returns>danh sách ReportActivitySummaries</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        public IEnumerable<ReportActivitySummaries> GetAllReportActivitySummaries()
        {
            var cachedData = _cache.Get("ReportActivitySummaries");
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                return JsonConvert.DeserializeObject<List<ReportActivitySummaries>>(cachedDataString);
            }
            else
            {
                var cachedDataString = JsonConvert.SerializeObject(ReportActivitySummaries.Where(m => m.FK_CompanyID == 15076));
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set("ReportActivitySummaries", newDataToCache);
                return ReportActivitySummaries.Where(m => m.FK_CompanyID == 15076);
            }
            //return ReportActivitySummaries;
        }



        /// <summary>config cấu hình đối tượng entity map db</summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.
        /// </param>
        /// <remarks>
        ///   <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)">UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)</see>)
        /// then this method will not be run. However, it will still run when creating a compiled model.
        /// </para>
        ///   <para>
        /// See <a href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</a> for more information and
        /// examples.
        /// </para>
        /// </remarks>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/24/2023 created
        /// </Modified>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicles>()
            .HasKey(i => i.PK_VehicleID);
            modelBuilder.Entity<BGTSpeedOvers>()
           .HasKey(i => new { i.FK_VehicleID, i.StartTime });

            modelBuilder.Entity<BGTTranportTypes>()
           .HasKey(i => new { i.PK_TransportTypeID })
            ;

            modelBuilder.Entity<BGTVehicleTransportTypes>()
           .HasKey(i => new { i.FK_VehicleID, i.FK_TransportTypeID });
            modelBuilder.Entity<BGTVehicleTransportTypes>().Property(p => p.IsDeleted).IsRequired(required: false);


            modelBuilder.Entity<ReportActivitySummaries>()
           .HasKey(i => new { i.FK_VehicleID, i.FK_Date });
             
        }



    }
}
