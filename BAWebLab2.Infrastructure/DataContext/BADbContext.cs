using BAWebLab2.DTO;
using BAWebLab2.Entity;
using BAWebLab2.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

namespace BAWebLab2.Infrastructure.DataContext
{
    /// <summary>class kết nối db</summary>
    public class BADbContext : DbContext
    {
        public BADbContext(DbContextOptions<BADbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDTO> User1s { get; set; }
        public DbSet<LoginResultDTO> User1s1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginResultDTO>().HasNoKey();
            // Configuration code...
        }

    }
}
