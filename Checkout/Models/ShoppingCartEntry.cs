using Checkout.Contracts;

namespace Checkout.Models
{
    public class ShoppingCartEntry : IShoppingCartEntry
    {
        public ShoppingCartEntry (string SKU, decimal UnitPrice)
        {
            this.SKU = SKU;
            this.UnitPrice = UnitPrice;
            this.Quantity = 0;
        }

        public string SKU { get; }

        public decimal UnitPrice { get; }

        public int Quantity { get; private set; }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }
    }
}
