using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Services;

namespace OrderManagement.Client.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            await _service.CreateAsync(order);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Cancel([FromBody] string id)
        {
            await _service.CancelOrderAsync(id);
            return Ok();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateDeliveryAddress(string id, [FromBody] Address newAddress)
        {
            await _service.UpdateDeliveryAddressAsync(id, newAddress);
            return Ok();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateOrderItems(string id, [FromBody] IEnumerable<OrderItem> items)
        {
            await _service.UpdateOrderItemsAsync(id, items);
            return Ok();
        }
    }
}