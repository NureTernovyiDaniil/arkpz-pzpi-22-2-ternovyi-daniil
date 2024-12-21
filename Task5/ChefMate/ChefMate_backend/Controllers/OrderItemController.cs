using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using ChefMate_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly OrdersService _ordersService;

        public OrderItemController(IOrderItemRepository orderItemRepository, OrdersService ordersService)
        {
            _orderItemRepository = orderItemRepository;
            _ordersService = ordersService;
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
            var result = await _orderItemRepository.Insert(orderItem);
            if(result)
            {
                await _ordersService.HandleOrder(orderItem.OrderId);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("post/list")]
        public async Task<IActionResult> PostList(List<OrderItemDto> orderItem)
        {
            var result = await _orderItemRepository.Insert(orderItem);
            if (result)
            {
                var orderId = orderItem.FirstOrDefault().OrderId;
                await _ordersService.HandleOrder(orderId);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderItemDto orderItem)
        {
            var result = await _orderItemRepository.Update(orderItem);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderItemRepository.Delete(id);

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(OrderItemDto orderItem)
        {
            var result = await _orderItemRepository.Delete(orderItem);

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
