namespace SportsStore.Controllers
{
    using System.Collections.Generic;
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

        public ViewResult List(string category = default, int page = 1)
        {
            IEnumerable<Product> targetProducts = _productRepository.Products;

            if (category != null)
            {
                targetProducts = targetProducts.Where(product => product.Category == category);
            }

            ProductsList productsList = new ProductsList
            {
                Products = targetProducts.OrderBy(product => product.Id)
                                         .Skip((page - 1) * PageSize)
                                         .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    ItemCount = _productRepository.Products.Count()
                },
                CurrentCategory = category
            };

            return View(productsList);
        }
    }
}