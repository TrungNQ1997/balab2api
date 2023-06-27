using BAWebLab2.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DAL.DataContext
{
    public class BADbContext : DbContext
    {
        public BADbContext(DbContextOptions<BADbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=BAWebManager;User ID=trungnq2;password=123");
        //}
    }
}
