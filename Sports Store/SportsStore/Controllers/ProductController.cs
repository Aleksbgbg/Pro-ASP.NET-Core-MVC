namespace SportsStore.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

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
            ProductsList productsList = new ProductsList
            {
                Products = _productRepository.Products
                                             .OrderBy(product => product.Id)
                                             .Skip((page - 1) * PageSize)
                                             .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    ItemCount = _productRepository.Products.Count()
                }
            };

            return View(productsList);
        }
    }
}