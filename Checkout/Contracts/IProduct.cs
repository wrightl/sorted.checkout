using System;

namespace Checkout.Contracts
{
    public interface IProduct
    {
        string SKU { get; set; }

        decimal UnitPrice { get; set; }
    }
}
