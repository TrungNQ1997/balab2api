using BAWebLab2.Entity;
using Microsoft.EntityFrameworkCore;

namespace BAWebLab2.DAL.DataContext
{
    /// <summary>class kết nối db</summary>
    public class BADbContext : DbContext
    {
        public BADbContext(DbContextOptions<BADbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        
    }
}
