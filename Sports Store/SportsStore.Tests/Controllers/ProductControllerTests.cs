namespace SportsStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;

    using Xunit;

    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(repository => repository.Products)
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
                                         },
                                         new Product
                                         {
                                                 Id = 4,
                                                 Name = "P4"
                                         },
                                         new Product
                                         {
                                                 Id = 5,
                                                 Name = "P5"
                                         }
                                 });

            ProductController controller = new ProductController(productRepositoryMock.Object);

            // Act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).ViewData.Model;

            // Assert
            Product[] products = result.ToArray();

            Assert.True(products.Length == 1);
            Assert.Equal("P5", products[0].Name);
        }
    }
}