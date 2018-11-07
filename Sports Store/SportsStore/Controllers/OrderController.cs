namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        private readonly Cart _cart;

        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _cart = cart;
        }

        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!_cart.CartLines.Any())
            {
                ModelState.AddModelError(string.Empty, "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.CartLines.ToArray();
                _orderRepository.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }

            return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }

        public ViewResult List()
        {
            return View(_orderRepository.Orders.Where(order => !order.Shipped));
        }

        [HttpPost]
        public IActionResult MarkShipped(int id)
        {
            Order targetOrder = _orderRepository.Orders
                                                .FirstOrDefault(order => order.Id == id);

            if (targetOrder != null)
            {
                targetOrder.Shipped = true;
                _orderRepository.SaveOrder(targetOrder);
            }

            return RedirectToAction(nameof(List));
        }
    }
}