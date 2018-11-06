namespace SportsStore.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using SportsStore.Controllers;
    using SportsStore.Models;

    using Xunit;

    public class OrderControllerTests
    {
        [Fact]
        public void CannotCheckoutEmptyCart()
        {
            // Arrange
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            Order order = new Order();

            OrderController controller = new OrderController(orderRepositoryMock.Object, cart);

            // Act
            ViewResult result = controller.Checkout(order) as ViewResult;

            // Assert
            orderRepositoryMock.Verify(orderRepository => orderRepository.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotCheckoutInvalidShippingDetails()
        {
            // Arrange
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController controller = new OrderController(orderRepositoryMock.Object, cart);

            controller.ModelState.AddModelError("error", "error");

            // Act
            ViewResult result = controller.Checkout(new Order()) as ViewResult;

            // Assert
            orderRepositoryMock.Verify(orderRepository => orderRepository.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            // Arrange
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController controller = new OrderController(orderRepositoryMock.Object, cart);

            // Act
            RedirectToActionResult result = controller.Checkout(new Order()) as RedirectToActionResult;

            // Assert
            orderRepositoryMock.Verify(orderRepository => orderRepository.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("Completed", result.ActionName);
        }
    }
}