namespace SportsStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;

    using Xunit;

    public class AdminControllerTests
    {
        [Fact]
        public void IndexContainsAllProducts()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(productRepository => productRepository.Products)
                                 .Returns(new Product[]
                                 {
                                     new Product
                                     {
                                         Id = 1,
                                         Name = "P1"
                                     },
                                     new Product
                                     {
                                         Id = 2,
                                         Name = "P2"
                                     },
                                     new Product
                                     {
                                         Id = 3,
                                         Name = "P3"
                                     }
                                 }.AsQueryable());

            AdminController controller = new AdminController(productRepositoryMock.Object);

            // Act
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}