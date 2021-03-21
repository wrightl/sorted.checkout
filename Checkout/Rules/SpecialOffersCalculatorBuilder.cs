using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Rules
{
    public class SpecialOffersCalculatorBuilder
    {
        private SpecialOffersCalculator _calculator = new SpecialOffersCalculator();

        public SpecialOffersCalculator Build() => _calculator;

        public SpecialOffersCalculatorBuilder WithBulkDiscount(string sku, decimal discount, int quantity)
        {
            _calculator.Rules.Add(new BulkDiscountOffer(sku, discount, quantity));
            return this;
        }
    }
}
