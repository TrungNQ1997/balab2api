using BAWebLab2.Model;
using Microsoft.EntityFrameworkCore;

using BAWebLab2.Entities;

namespace BAWebLab2.Infrastructure.DataContext
{

    /// <summary>class kết nối db</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class BADbContext : DbContext
    {
        public BADbContext(DbContextOptions<BADbContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }

        public DbSet<BGTSpeedOvers> BGTSpeedOvers { get; set; }

        public DbSet<BGTTranportTypes> BGTTranportTypes { get; set; }

        public DbSet<BGTVehicleTransportTypes> BGTVehicleTransportTypes { get; set; }

        public DbSet<ReportActivitySummaries> ReportActivitySummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicles>()
            .HasKey(i => i.PK_VehicleID);
            modelBuilder.Entity<BGTSpeedOvers>()
           .HasKey(i => new { i.FK_VehicleID, i.StartTime });


            modelBuilder.Entity<BGTSpeedOvers>().Property(p => p.Description).IsRequired(required: false);
            modelBuilder.Entity<BGTSpeedOvers>().Property(p => p.TotalTime).IsRequired(required: false);
            modelBuilder.Entity<BGTSpeedOvers>().Property(p => p.Distances).IsRequired(required: false);

            //modelBuilder.Entity<BGTSpeedOvers>().HasNoKey();

            modelBuilder.Entity<BGTTranportTypes>()
           .HasKey(i => new { i.PK_TransportTypeID });

            modelBuilder.Entity<BGTVehicleTransportTypes>()
           .HasKey(i => new { i.FK_VehicleID, i.FK_TransportTypeID });
            modelBuilder.Entity<BGTVehicleTransportTypes>().Property(p => p.IsDeleted).IsRequired(required: false);


            modelBuilder.Entity<ReportActivitySummaries>()
           .HasKey(i => new { i.FK_VehicleID, i.FK_Date });

            modelBuilder.Entity<ReportActivitySummaries>().Property(p => p.UpdateDate).IsRequired(required: false);
            modelBuilder.Entity<ReportActivitySummaries>().Property(p => p.ChangeType).IsRequired(required: false);

            modelBuilder.Entity<LoginResult>().HasNoKey();
            // Configuration code...
        }

    }
}
