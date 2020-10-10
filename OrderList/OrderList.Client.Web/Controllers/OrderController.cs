using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderList.Domain.Services;

namespace OrderList.Client.Web.Controllers
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _service.GetAsync(id));
        }
        
        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> Get(int page, int size)
        {
            return Ok(await _service.GetAsync(page, size));
        }
    }
}