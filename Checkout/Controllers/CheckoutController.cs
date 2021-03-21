using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Checkout.Contracts;
using Checkout.Models;

namespace Checkout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private static readonly List<IProduct> _productsInMemoryStore = new List<IProduct>();

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;

            if (_productsInMemoryStore.Count == 0) {
                _productsInMemoryStore.Add(new Product() { SKU = "A99", UnitPrice = 0.5 });
                _productsInMemoryStore.Add(new Product() { SKU = "B15", UnitPrice = 0.3 });
                _productsInMemoryStore.Add(new Product() { SKU = "C40", UnitPrice = 0.6 });
            }
        }

        [HttpPost]
        [Route("{sku}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
                     nameof(DefaultApiConventions.Create))]
        public ActionResult Add([FromRoute]string sku)
        {
            var product = _productsInMemoryStore.FirstOrDefault(p => p.SKU == sku);

            if (product == null)
            {
                _logger.LogError("SKU not found: " + sku);
                return BadRequest(sku);
            }

            return this.CreatedAtAction(nameof(this.Get), new { product.SKU }, product);
        }

        [HttpGet]
        public IEnumerable<IProduct> Get()
        {
            // use session to get list of products



            return _productsInMemoryStore;
        }

        [HttpGet]
        [Route("total")]
        public double GetPrice()
        {
            // use session to get list of products



            return 1000;
        }
    }
}
