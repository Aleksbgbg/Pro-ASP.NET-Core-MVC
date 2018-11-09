namespace SportsStore.Models
{
    using System.Linq;

    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int id);
    }
}