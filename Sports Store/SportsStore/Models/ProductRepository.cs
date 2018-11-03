namespace SportsStore.Models
{
    using System.Collections.Generic;

    using SportsStore.Data;

    public class ProductRepository : IProductRepository
    {
        private readonly SportsStoreDbContext _dbContext;

        public ProductRepository(SportsStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> Products => _dbContext.Products;
    }
}