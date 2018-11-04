namespace SportsStore.Tests.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ViewComponents;

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
                                 });

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(productRepositoryMock.Object);

            // Act
            string[] results = ((IEnumerable<string>)((ViewViewComponentResult)target.Invoke()).ViewData.Model).ToArray();

            // Assert
            Assert.True(new string[] { "Apples", "Oranges", "Plums" }.SequenceEqual(results));
        }
    }
}