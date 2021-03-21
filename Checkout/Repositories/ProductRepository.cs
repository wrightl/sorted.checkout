using Checkout.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly IList<IProduct> _productsInMemoryStore = new List<IProduct>();

        public IProduct GetById(string SKU)
        {
            IProduct product = _productsInMemoryStore.FirstOrDefault(p => SKU.Equals(p.SKU));

            if (product == null)
            {
                return null;
            }

            return product;
        }
        
        public void Save(IProduct product) 
        {
            _productsInMemoryStore.Add(product);
        }
    }
}
