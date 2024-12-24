using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orgs = await _organizationRepository.Retrieve();
            return Ok(orgs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var org = await _organizationRepository.Retrieve(id);

            if (org != null)
            {
                return Ok(org);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrganizationDto organizationDto)
        {
            var result = await _organizationRepository.Insert(organizationDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(OrganizationDto organizationDto)
        {
            var result = await _organizationRepository.Update(organizationDto);


            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _organizationRepository.Delete(id);
            return Ok(result);
        }
    }
}
