namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class ProductController : Controller
    {
        private const int PageSize = 4;

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult List(int page = 1)
        {
            return View(_productRepository.Products
                                          .OrderBy(product => product.Id)
                                          .Skip((page - 1) * PageSize)
                                          .Take(PageSize));
        }
    }
}