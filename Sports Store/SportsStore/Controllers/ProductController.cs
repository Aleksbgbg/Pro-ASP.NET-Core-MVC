namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult List()
        {
            return View(_productRepository.Products);
        }
    }
}