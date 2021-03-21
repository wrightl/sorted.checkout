using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Checkout;
using Checkout.Services;
using Checkout.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ShoppingCartServiceFixture
    {

        private decimal discount = 0.2M;

        private ShoppingCartService _sut;

        public ShoppingCartServiceFixture()
        {
            _sut = null;
        }

        public ShoppingCartService SystemUnderTest
        {
            get
            {
                if (_sut == null)
                {
                    _sut = new ShoppingCartService(
                                SpecialOffersInstance.Object);
                }

                return _sut;
            }
        }

        private Mock<ISpecialOffersCalculator> mockSpecialOffers;

        public Mock<ISpecialOffersCalculator> SpecialOffersInstance
        {
            get
            {
                if (mockSpecialOffers == null)
                {
                    mockSpecialOffers = new Mock<ISpecialOffersCalculator>();
                    mockSpecialOffers.Setup(c => c.CalculateDiscount(It.IsAny<IShoppingCartEntry>())).Returns(discount);
                }

                return mockSpecialOffers;
            }
        }

        [Fact]
        public void Add_ShouldReturnCartEntry_WhenGivenValidProductCode()
        {
            // Arrange
            var service = SystemUnderTest;

            // Act
            var entry = service.Add(ProductConstants.A99_SKU, ProductConstants.A99_UnitPrice);
            var actualTotal = service.GetTotal();

            // Assert
            var expectedTotal = ProductConstants.A99_UnitPrice - discount;

            Assert.Equal(expectedTotal, actualTotal);
            Assert.NotNull(entry);
        }

        [Fact]
        public void Add_ShouldNoCartEntry_WhenGivenInvalidProductCode()
        {
            // Arrange
            var service = SystemUnderTest;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.Add("", 1));

            Assert.Equal("No SKU provided (Parameter 'SKU')", ex.Message);
        }

        [Fact]
        public void Add_ShouldNoCartEntry_WhenGivenInvalidUnitPrice()
        {
            // Arrange
            var service = SystemUnderTest;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.Add(ProductConstants.A99_SKU, -1));

            Assert.Equal("UnitPrice must be greater than zero (Parameter 'UnitPrice')", ex.Message);
        }
    }
}
