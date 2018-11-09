namespace SportsStore.Models
{
    using System.Linq;

    using SportsStore.Data;

    public class ProductRepository : IProductRepository
    {
        private readonly SportsStoreDbContext _dbContext;

        public ProductRepository(SportsStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _dbContext.Products.Add(product);
            }
            else
            {
                Product targetProduct = _dbContext.Products
                                                  .FirstOrDefault(dbProduct => dbProduct.Id == product.Id);

                if (targetProduct != null)
                {
                    targetProduct.Name = product.Name;
                    targetProduct.Description = product.Description;
                    targetProduct.Price = product.Price;
                    targetProduct.Category = product.Category;
                }
            }

            _dbContext.SaveChanges();
        }

        public Product DeleteProduct(int id)
        {
            Product targetProduct = _dbContext.Products
                                              .FirstOrDefault(product => product.Id == id);

            if (targetProduct != null)
            {
                _dbContext.Products.Remove(targetProduct);
                _dbContext.SaveChanges();
            }

            return targetProduct;
        }
    }
}