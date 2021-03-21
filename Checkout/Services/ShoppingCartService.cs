using Checkout.Contracts;
using Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private static List<IShoppingCartEntry> _cart = new List<IShoppingCartEntry>();

        public IShoppingCartEntry Add(string SKU, decimal UnitPrice, int Quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(SKU))
            {
                throw new ArgumentException("No SKU provided", "SKU");
            }
            else if (UnitPrice < 0)
            {
                throw new ArgumentException("UnitPrice must be greater than zero", "UnitPrice");
            }

            var shoppingCartEntry = _cart.FirstOrDefault(e => e.SKU == SKU);

            if (shoppingCartEntry == null)
            {
                shoppingCartEntry = new ShoppingCartEntry(SKU, UnitPrice);
                _cart.Add(shoppingCartEntry);
            }

            shoppingCartEntry.AddQuantity(Quantity);

            return shoppingCartEntry;
        }

        public IEnumerable<IShoppingCartEntry> GetAll()
        {
            return _cart;
        }

        public decimal GetTotal()
        {
            return _cart.Sum(e => e.Quantity * e.UnitPrice );
        }
    }
}
