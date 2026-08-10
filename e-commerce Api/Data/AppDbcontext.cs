using e_commerce_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<TokenHistory> TokenHistories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}