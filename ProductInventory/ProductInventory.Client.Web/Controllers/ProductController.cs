using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    }
}