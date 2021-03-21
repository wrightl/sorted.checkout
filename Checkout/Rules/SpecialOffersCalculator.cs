using Checkout.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Rules
{
    public class SpecialOffersCalculator : ISpecialOffersCalculator
    {
        private List<ISpecialOffer> _rules = new List<ISpecialOffer>();

        public List<ISpecialOffer> Rules => _rules;

        public decimal CalculateDiscount(IShoppingCartEntry cartEntry)
        {
            decimal discount = 0;

            foreach (var rule in _rules)
            {
                discount += rule.CalculateDiscount(cartEntry);
            }

            return discount;
        }
    }
}
