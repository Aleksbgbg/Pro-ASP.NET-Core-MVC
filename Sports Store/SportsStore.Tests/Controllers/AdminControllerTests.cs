namespace SportsStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        [Fact]
        public void CanEditProduct()
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
            Product p1 = GetViewModel<Product>(controller.Edit(1));
            Product p2 = GetViewModel<Product>(controller.Edit(2));
            Product p3 = GetViewModel<Product>(controller.Edit(3));

            // Assert
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);
        }

        [Fact]
        public void CannotEditNonExistentProduct()
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
            Product result = GetViewModel<Product>(controller.Edit(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempDataDictionaryMock = new Mock<ITempDataDictionary>();

            AdminController controller = new AdminController(productRepositoryMock.Object)
            {
                TempData = tempDataDictionaryMock.Object
            };

            Product product = new Product
            {
                Name = "Test"
            };

            // Act
            IActionResult result = controller.Edit(product);

            // Assert
            productRepositoryMock.Verify(productRepository => productRepository.SaveProduct(product));

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            AdminController controller = new AdminController(productRepositoryMock.Object);

            Product product = new Product
            {
                Name = "Test"
            };

            controller.ModelState.AddModelError("error", "error");

            // Act
            IActionResult result = controller.Edit(product);

            // Assert
            productRepositoryMock.Verify(productRepository => productRepository.SaveProduct(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}