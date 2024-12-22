using ChefMate_backend.Hubs;
using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using iText.Layout.Borders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderController(IOrderRepository orderRepository, IServiceScopeFactory scopeFactory)
        {
            _orderRepository = orderRepository;
            _scopeFactory = scopeFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderRepository.Retrieve();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderRepository.Retrieve(id);
            return Ok(order);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(OrderDto order)
        {
            var result = await _orderRepository.Insert(order);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderDto order)
        {
            var result = await _orderRepository.Update(order);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderRepository.Delete(id);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(OrderDto order)
        {
            var result = await _orderRepository.Delete(order);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
