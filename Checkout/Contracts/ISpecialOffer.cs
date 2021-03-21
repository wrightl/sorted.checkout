using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Contracts
{
    public interface ISpecialOffer
    {
        decimal CalculateDiscount(IShoppingCartEntry cartEntry);
    }
}
