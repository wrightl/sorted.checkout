using Checkout.Contracts;

namespace Checkout.Contracts
{
    public interface ISpecialOffersCalculator
    {
        decimal CalculateDiscount(IShoppingCartEntry cartEntry);
    }
}