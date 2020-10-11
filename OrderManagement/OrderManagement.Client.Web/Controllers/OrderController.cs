using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Services;

namespace OrderManagement.Client.Web.Controllers
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            try
            {
                await _service.CreateAsync(order);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to create order");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Cancel([FromBody] string id)
        {
            try
            {
                await _service.CancelOrderAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to cancel order");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateDeliveryAddress(string id, [FromBody] Address newAddress)
        {
            try
            {
                await _service.UpdateDeliveryAddressAsync(id, newAddress);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to update delivery address");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateOrderItems(string id, [FromBody] IEnumerable<OrderItem> items)
        {
            try
            {
                await _service.UpdateOrderItemsAsync(id, items);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error trying to update order items");
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}