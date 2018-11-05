namespace SportsStore.Models
{
    using System.Linq;

    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}