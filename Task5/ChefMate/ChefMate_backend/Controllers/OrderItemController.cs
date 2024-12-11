using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderItems = await _orderItemRepository.Retrieve();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var orderItem = await _orderItemRepository.Retrieve(id);
            return Ok(orderItem);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(OrderItemDto orderItem)
        {
            await _orderItemRepository.Insert(orderItem);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(OrderItemDto orderItem)
        {
            await _orderItemRepository.Update(orderItem);
            return Ok();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orderItem = await _orderItemRepository.Retrieve(id);
            await _orderItemRepository.Delete(orderItem);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(OrderItemDto orderItem)
        {
            await _orderItemRepository.Delete(orderItem);
            return Ok();
        }
    }
}
