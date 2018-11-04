namespace SportsStore.Tests.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

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
            ProductsList result = (ProductsList)controller.List(page: 2).ViewData.Model;

            // Assert
            Product[] products = result.Products.ToArray();

            Assert.True(products.Length == 1);
            Assert.Equal("P5", products[0].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
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
            ProductsList result = (ProductsList)controller.List(page: 2).ViewData.Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(4, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.ItemCount);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void CanFilterProducts()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(productRepository => productRepository.Products)
                                 .Returns(new Product[]
                                 {
                                     new Product
                                     {
                                         Id = 1,
                                         Name = "P1",
                                         Category = "Cat1"
                                     },
                                     new Product
                                     {
                                         Id = 2,
                                         Name = "P2",
                                         Category = "Cat2"
                                     },
                                     new Product
                                     {
                                         Id = 3,
                                         Name = "P3",
                                         Category = "Cat1"
                                     },
                                     new Product
                                     {
                                         Id = 4,
                                         Name = "P4",
                                         Category = "Cat2"
                                     },
                                     new Product
                                     {
                                         Id = 5,
                                         Name = "P5",
                                         Category = "Cat3"
                                     }
                                 });

            ProductController controller = new ProductController(productRepositoryMock.Object);

            // Act
            Product[] result = ((ProductsList)controller.List("Cat2").ViewData.Model).Products.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void GenerateCategorySpecificProductCount()
        {
            // Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(productRepository => productRepository.Products)
                                 .Returns(new Product[]
                                 {
                                     new Product
                                     {
                                         Id = 1,
                                         Name = "P1",
                                         Category = "Cat1"
                                     },
                                     new Product
                                     {
                                         Id = 2,
                                         Name = "P2",
                                         Category = "Cat2"
                                     },
                                     new Product
                                     {
                                         Id = 3,
                                         Name = "P3",
                                         Category = "Cat1"
                                     },
                                     new Product
                                     {
                                         Id = 4,
                                         Name = "P4",
                                         Category = "Cat2"
                                     },
                                     new Product
                                     {
                                         Id = 5,
                                         Name = "P5",
                                         Category = "Cat3"
                                     }
                                 });

            ProductController controller = new ProductController(productRepositoryMock.Object);

            int? GetTotalItems(string category)
            {
                return ((ProductsList)controller.List(category)?.ViewData?.Model)?.PagingInfo.ItemCount;
            }

            // Act
            int? category1Items = GetTotalItems("Cat1");
            int? category2Items = GetTotalItems("Cat2");
            int? category3Items = GetTotalItems("Cat3");
            int? allCategoryItems = GetTotalItems(null);

            // Assert
            Assert.Equal(2, category1Items);
            Assert.Equal(2, category2Items);
            Assert.Equal(1, category3Items);
            Assert.Equal(5, allCategoryItems);
        }
    }
}