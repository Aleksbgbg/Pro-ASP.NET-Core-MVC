namespace SportsStore.Models
{
    using System.Collections.Generic;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}