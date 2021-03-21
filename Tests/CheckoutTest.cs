using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Checkout.Controllers;
using Checkout.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CheckoutTest
    {
        [Fact]
        public async Task Get_ShouldReturnThreeProducts()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CheckoutController>>();
            var controller = new CheckoutController(mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            var products = Assert.IsAssignableFrom<IEnumerable<IProduct>>(result);
            Assert.Equal(3, products.Count());
        }

        [Theory]
        [InlineData("A99")]
        public async Task Add_ShouldReturnCreatedResponse_WhenGivenValidProductCode(string SKU)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CheckoutController>>();
            var controller = new CheckoutController(mockLogger.Object);

            // Act
            var response = controller.Add(SKU);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Theory]
        [InlineData("A99_Bad")]
        public async Task Add_ShouldReturnBadRequest_WhenGivenInValidProductCode(string SKU)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CheckoutController>>();
            var controller = new CheckoutController(mockLogger.Object);

            // Act
            var response = controller.Add(SKU);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
