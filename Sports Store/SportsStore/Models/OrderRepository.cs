namespace SportsStore.Models
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using SportsStore.Data;

    public class OrderRepository : IOrderRepository
    {
        private readonly SportsStoreDbContext _dbContext;

        public OrderRepository(SportsStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Order> Orders => _dbContext.Orders
                                                     .Include(order => order.Lines)
                                                     .ThenInclude(cartLine => cartLine.Product);

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(cartLine => cartLine.Product));

            if (order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }

            _dbContext.SaveChanges();
        }
    }
}