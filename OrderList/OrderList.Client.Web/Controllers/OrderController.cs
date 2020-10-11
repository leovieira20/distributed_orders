using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderList.Domain.Services;

namespace OrderList.Client.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _service.GetAsync(id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error trying to get order {id}");
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> Get(int page, int size)
        {
            try
            {
                return Ok(await _service.GetAsync(page, size));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error trying to get orders, page: {page}, size: {size}");
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}