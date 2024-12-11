using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
            await _orderRepository.Insert(order);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(OrderDto order)
        {
            await _orderRepository.Update(order);
            return Ok();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderRepository.Retrieve(id);
            await _orderRepository.Delete(order);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(OrderDto order)
        {
            await _orderRepository.Delete(order);
            return Ok();
        }
    }
}
