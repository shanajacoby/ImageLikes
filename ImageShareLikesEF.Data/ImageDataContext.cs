using Microsoft.EntityFrameworkCore;
using System;

namespace ImageShareLikesEF.Data
{
    public class ImageDataContext : DbContext
    {
        private string _connectionString;
        public ImageDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Image> Images { get; set; }
    }
}
