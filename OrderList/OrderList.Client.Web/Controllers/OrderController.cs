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
        public IActionResult Get(string id)
        {
            return Ok(_service.Get(id));
        }
        
        [HttpGet("{page}/{size}")]
        public IActionResult Get(int page, int size)
        {
            return Ok(_service.Get(page, size));
        }
    }
}