using Checkout.Contracts;

namespace Checkout.Rules
{
    public class BulkDiscountOffer : ISpecialOffer
    {
        public BulkDiscountOffer(string SKU, decimal discount, int quantity)
        {
            this.SKU = SKU;
            this.Discount = discount;
            this.Quantity = quantity;
        }

        public string SKU { get; }

        public decimal Discount { get; }

        public int Quantity { get; }

        public decimal CalculateDiscount(IShoppingCartEntry cartEntry)
        {
            if (cartEntry.SKU != SKU)
                return 0;

            return this.Discount * (cartEntry.Quantity / this.Quantity);
        }
    }
}
