using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Checkout.Services;
using Checkout.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ShoppingCartServiceFixture
    {
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
                    _sut = new ShoppingCartService();
                }

                return _sut;
            }
        }

        [Fact]
        public void Add_ShouldReturnCartEntry_WhenGivenValidProductCode()
        {
            // Arrange
            var service = SystemUnderTest;

            // Act
            var entry = service.Add("A99", 0.5M);
            var actualTotal = service.GetTotal();

            // Assert
            var expectedTotal = 0.5M;

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
            var ex = Assert.Throws<ArgumentException>(() => service.Add("A99", -1));

            Assert.Equal("UnitPrice must be greater than zero (Parameter 'UnitPrice')", ex.Message);
        }
    }
}
