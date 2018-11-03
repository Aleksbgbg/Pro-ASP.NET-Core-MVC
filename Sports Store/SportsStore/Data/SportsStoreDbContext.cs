namespace SportsStore.Data
{
    using Microsoft.EntityFrameworkCore;

    using SportsStore.Models;

    public class SportsStoreDbContext : DbContext
    {
        public SportsStoreDbContext(DbContextOptions<SportsStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}