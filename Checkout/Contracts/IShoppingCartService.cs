using System.Collections.Generic;

namespace Checkout.Contracts
{
    public interface IShoppingCartService
    {
        IShoppingCartEntry Add(string SKU, decimal UnitPrice, int Quantity = 1);

        IEnumerable<IShoppingCartEntry> GetAll();

        decimal GetTotal();
    }
}