using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerRepository.Retrieve();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customerRepository.Retrieve(id);
            return Ok(customer);
        }
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerRepository.Retrieve(id);
            await _customerRepository.Delete(customer);
            return Ok();
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(CustomerDto customer)
        {
            await _customerRepository.Delete(customer);
            return Ok();
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(CustomerDto customer)
        {
            await _customerRepository.Insert(customer);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(CustomerDto customer)
        {
            await _customerRepository.Update(customer);
            return Ok();
        }
    }
}
