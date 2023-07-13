using BAWebLab2.Model;
using BAWebLab2.Entity;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<User> Users { get; set; }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginResult>().HasNoKey();
            // Configuration code...
        }

    }
}
