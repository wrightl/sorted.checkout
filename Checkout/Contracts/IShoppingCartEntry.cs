namespace Checkout.Contracts
{
    public interface IShoppingCartEntry
    {
        string SKU { get; }

        decimal UnitPrice { get; }

        int Quantity { get; }

        void AddQuantity(int quantity);
    }
}
