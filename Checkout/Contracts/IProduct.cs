using System;

namespace Checkout.Contracts
{
    public interface IProduct
    {
        string SKU { get; set; }

        double UnitPrice { get; set; }
    }
}
