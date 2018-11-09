﻿namespace SportsStore.Controllers
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

        public ViewResult Create()
        {
            return View(nameof(Edit), new Product());
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

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.SaveProduct(product);

                TempData["message"] = $"{product.Name} has been saved";

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
    }
}