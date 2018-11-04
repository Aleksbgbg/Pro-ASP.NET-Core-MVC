namespace SportsStore.Tests.Models
{
    using System.Linq;

    using SportsStore.Models;

    using Xunit;

    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            // Arrange
            Product p1 = new Product
            {
                Id = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                Id = 2,
                Name = "P2"
            };

            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] results = target.CartLines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange
            Product p1 = new Product
            {
                Id = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                Id = 2,
                Name = "P2"
            };

            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.AddItem(p1, 10);

            CartLine[] results = target.CartLines.OrderBy(cart => cart.Product.Id).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            // Arrange
            Product p1 = new Product
            {
                Id = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                Id = 2,
                Name = "P2"
            };

            Product p3 = new Product
            {
                Id = 3,
                Name = "P3"
            };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Equal(0, target.CartLines.Count(cart => cart.Product == p2));
            Assert.Equal(2, target.CartLines.Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            // Arrange
            Product p1 = new Product
            {
                Id = 1,
                Name = "P1",
                Price = 100M
            };

            Product p2 = new Product
            {
                Id = 2,
                Name = "P2",
                Price = 50M
            };

            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal result = target.TotalCost;

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            // Arrange
            Product p1 = new Product
            {
                Id = 1,
                Name = "P1",
                Price = 100M
            };

            Product p2 = new Product
            {
                Id = 2,
                Name = "P2",
                Price = 50M
            };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            // Assert
            Assert.Empty(target.CartLines);
        }
    }
}