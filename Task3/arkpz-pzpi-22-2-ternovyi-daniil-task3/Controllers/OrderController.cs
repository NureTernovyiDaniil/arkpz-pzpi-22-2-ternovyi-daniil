﻿using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderRepository.Retrieve();
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderRepository.Retrieve(id);
            return Ok(order);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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