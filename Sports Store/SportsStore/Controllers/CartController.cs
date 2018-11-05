namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly Cart _cart;

        public CartController(IProductRepository productRepository, Cart cart)
        {
            _productRepository = productRepository;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndex
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Product targetProduct = _productRepository.Products
                                                      .FirstOrDefault(product => product.Id == id);

            if (targetProduct != null)
            {
                _cart.AddItem(targetProduct, 1);
            }

            return RedirectToAction("Index", new
            {
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            Product targetProduct = _productRepository.Products
                                                      .FirstOrDefault(product => product.Id == id);

            if (targetProduct != null)
            {
                _cart.RemoveLine(targetProduct);
            }

            return RedirectToAction("Index", new
            {
                ReturnUrl = returnUrl
            });
        }
    }
}