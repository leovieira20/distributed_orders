using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Client.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repository;

        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            try
            {
                await _repository.CreateAsync(product);
                return Created($"/api/ProductInventory/{product.ProductId}", product);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to create product");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to get products");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _repository.GetAsync(id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to get products");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}