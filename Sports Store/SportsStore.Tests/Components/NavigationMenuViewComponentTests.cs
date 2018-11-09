namespace SportsStore.Tests.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Routing;

    using Moq;

    using SportsStore.Components;
    using SportsStore.Models;

    using Xunit;

    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void CanSelectCategories()
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
                                         Category = "Apples"
                                     },
                                     new Product
                                     {
                                         Id = 2,
                                         Name = "P2",
                                         Category = "Apples"
                                     },
                                     new Product
                                     {
                                         Id = 3,
                                         Name = "P3",
                                         Category = "Plums"
                                     },
                                     new Product
                                     {
                                         Id = 4,
                                         Name = "P4",
                                         Category = "Oranges"
                                     },
                                 }.AsQueryable());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(productRepositoryMock.Object);

            // Act
            string[] results = ((IEnumerable<string>)((ViewViewComponentResult)target.Invoke()).ViewData.Model).ToArray();

            // Assert
            Assert.True(new string[] { "Apples", "Oranges", "Plums" }.SequenceEqual(results));
        }

        [Fact]
        public void IndicatesSelectedCategory()
        {
            // Arrange
            const string categoryToSelect = "Apples";

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(productRepository => productRepository.Products)
                                 .Returns(new Product[]
                                 {
                                     new Product
                                     {
                                         Id = 1,
                                         Name = "P1",
                                         Category = "Apples"
                                     },
                                     new Product
                                     {
                                         Id = 4,
                                         Name = "P2",
                                         Category = "Oranges"
                                     }
                                 }.AsQueryable());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(productRepositoryMock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        RouteData = new RouteData()
                    }
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            // Act
            string result = (string)((ViewViewComponentResult)target.Invoke()).ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}