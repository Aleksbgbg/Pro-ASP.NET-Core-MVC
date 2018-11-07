namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepository;

        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index()
        {
            return View(_productRepository.Products);
        }

        public ViewResult Edit(int id)
        {
            return View(_productRepository.Products
                                          .FirstOrDefault(product => product.Id == id));
        }
    }
}