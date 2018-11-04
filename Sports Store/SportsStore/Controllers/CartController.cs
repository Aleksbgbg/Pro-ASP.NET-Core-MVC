namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Extensions;
    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndex
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Product targetProduct = _productRepository.Products
                                                      .FirstOrDefault(product => product.Id == id);

            if (targetProduct != null)
            {
                Cart cart = GetCart();
                cart.AddItem(targetProduct, 1);
                SaveCart(cart);
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
                Cart cart = GetCart();
                cart.RemoveLine(targetProduct);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new
            {
                ReturnUrl = returnUrl
            });
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}