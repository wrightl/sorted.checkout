using System;
using Checkout.Contracts;

namespace Checkout.Models
{
    public class Product : IProduct
    {
        public string SKU { get; set; }

        public double UnitPrice { get; set; }
    }
}
