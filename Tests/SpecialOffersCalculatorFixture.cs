using Moq;
using Checkout;
using Checkout.Contracts;
using Checkout.Models;
using Checkout.Rules;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class SpecialOffersCalculatorFixture
    {
        private SpecialOffersCalculator _sut;

        public SpecialOffersCalculatorFixture()
        {
            _sut = null;
        }

        public SpecialOffersCalculator SystemUnderTest
        {
            get
            {
                if (_sut == null)
                {
                    _sut = new SpecialOffersCalculatorBuilder()
                                .WithBulkDiscount(ProductConstants.A99_SKU, 0.2M, 3)
                                .WithBulkDiscount(ProductConstants.B15_SKU, 0.15M, 2)
                                .Build();
                }

                return _sut;
            }
        }

        [Fact]
        public void CalculateDiscount_ShouldReturnZero_WhenQuantityEqualsOneForA99()
        {
            // Arrange
            var mockCart = CreatMockShoppingCartEntry(ProductConstants.A99_SKU, ProductConstants.A99_UnitPrice, 1);

            // Act
            var result = this.SystemUnderTest.CalculateDiscount(mockCart.Object);

            // Assert
            var expectedDiscount = 0;
            Assert.Equal(expectedDiscount, result);
        }

        [Fact]
        public void CalculateDiscount_ShouldReturnZeroPointTwo_WhenQuantityEqualsThreeForA99()
        {
            // Arrange
            var mockCart = CreatMockShoppingCartEntry(ProductConstants.A99_SKU, ProductConstants.A99_UnitPrice, 3);

            // Act
            var result = this.SystemUnderTest.CalculateDiscount(mockCart.Object);

            // Assert
            var expectedDiscount = 0.2M;
            Assert.Equal(expectedDiscount, result);
        }

        [Fact]
        public void CalculateDiscount_ShouldReturnZero_WhenQuantityEqualsOneForB15()
        {
            // Arrange
            var mockCart = CreatMockShoppingCartEntry(ProductConstants.B15_SKU, ProductConstants.B15_UnitPrice, 1);

            // Act
            var result = this.SystemUnderTest.CalculateDiscount(mockCart.Object);

            // Assert
            var expectedDiscount = 0;
            Assert.Equal(expectedDiscount, result);
        }

        [Fact]
        public void CalculateDiscount_ShouldReturnZeroPointOneFive_WhenQuantityEqualsThreeForB15()
        {
            // Arrange
            var mockCart = CreatMockShoppingCartEntry(ProductConstants.B15_SKU, ProductConstants.B15_UnitPrice, 3);

            // Act
            var result = this.SystemUnderTest.CalculateDiscount(mockCart.Object);

            // Assert
            var expectedDiscount = 0.15M;
            Assert.Equal(expectedDiscount, result);
        }

        private Mock<IShoppingCartEntry> CreatMockShoppingCartEntry(string sku, decimal discount, int quantity)
        {
            var mockCart = new Mock<IShoppingCartEntry>();
            mockCart.SetupGet<string>(e => e.SKU).Returns(sku);
            mockCart.SetupGet<decimal>(e => e.UnitPrice).Returns(discount);
            mockCart.SetupGet<int>(e => e.Quantity).Returns(quantity);

            return mockCart;
        }
    }
}
