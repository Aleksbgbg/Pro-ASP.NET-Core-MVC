namespace SportsStore.Models
{
    using System.Collections.Generic;

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products => new Product[]
        {
            new Product
            {
                Name = "Football",
                Price = 25m
            },
            new Product
            {
                Name = "Surf Board",
                Price = 179m
            },
            new Product
            {
                Name = "Running Shoes",
                Price = 95m
            }
        };
    }
}