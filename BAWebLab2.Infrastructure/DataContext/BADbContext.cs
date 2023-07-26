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
         
        public BADbContext(DbContextOptions<BADbContext> options ) : base(options)
        {
 
        }

        ////public DbSet<User> Users { get; set; }
        ///// <summary>dbset chứa vehicles từ db</summary>
        ///// <value>The vehicles.</value>
        ///// <Modified>
        ///// Name Date Comments
        ///// trungnq3 7/24/2023 created
        ///// </Modified>
        //public DbSet<Vehicles> Vehicles { get; set; }

        ///// <summary>dbset chứa danh sách BGTspeedovers lấy từ db</summary>
        ///// <value>BGTspeedovers.</value>
        ///// <Modified>
        ///// Name Date Comments
        ///// trungnq3 7/24/2023 created
        ///// </Modified>
        //public DbSet<BGTSpeedOvers> BGTSpeedOvers { get; set; }

        ///// <summary>dbset chứa danh sách BGTtranporttypes lấy từ db</summary>
        ///// <value>BGTtranporttypes.</value>
        ///// <Modified>
        ///// Name Date Comments
        ///// trungnq3 7/24/2023 created
        ///// </Modified>
        //public DbSet<BGTTranportTypes> BGTTranportTypes { get; set; }

        ///// <summary>dbset chứa BGTvehicletransporttypes lấy từ db</summary>
        ///// <value>BGTvehicletransporttypes.</value>
        ///// <Modified>
        ///// Name Date Comments
        ///// trungnq3 7/24/2023 created
        ///// </Modified>
        //public DbSet<BGTVehicleTransportTypes> BGTVehicleTransportTypes { get; set; }

        ///// <summary>dbset chứa danh sách report.activitysummaries lấy từ db</summary>
        ///// <value>report.activitysummaries.</value>
        ///// <Modified>
        ///// Name Date Comments
        ///// trungnq3 7/24/2023 created
        ///// </Modified>
        //public DbSet<ReportActivitySummaries> ReportActivitySummaries { get; set; }
         
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
