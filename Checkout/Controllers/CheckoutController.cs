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
        private readonly ILogger<CheckoutController> _logger;
        private readonly IShoppingCartService _service;
        private readonly IProductRepository _repository;

        public CheckoutController(ILogger<CheckoutController> logger, IShoppingCartService service, IProductRepository repository)
        {
            _logger = logger;
            _service = service;
            _repository = repository;
        }

        [HttpPost]
        [Route("{sku}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
                     nameof(DefaultApiConventions.Create))]
        public ActionResult Add([FromRoute]string sku)
        {
            var product = _repository.GetById(sku);

            if (product == null)
            {
                string msg = "SKU not found: " + sku;
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            var cartEntry = _service.Add(product.SKU, product.UnitPrice);

            return this.CreatedAtAction(nameof(this.Get), new { cartEntry.SKU }, cartEntry);
        }

        [HttpGet]
        public IEnumerable<IShoppingCartEntry> Get()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("total")]
        public decimal GetPrice()
        {
            return _service.GetTotal();
        }
    }
}
