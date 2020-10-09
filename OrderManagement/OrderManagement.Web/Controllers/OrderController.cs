using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Services;

namespace OrderManagement.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _service.CreateAsync(order);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _service.CancelOrderAsync(id);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateDeliveryAddress(Guid id, Address newAddress)
        {
            await _service.UpdateDeliveryAddressAsync(id, newAddress);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateOrderItems(Guid id, IEnumerable<OrderItem> items)
        {
            await _service.UpdateOrderItemsAsync(id, items);
            return Ok();
        }
    }
}