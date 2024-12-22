using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkZoneController : ControllerBase
    {
        private readonly IWorkZoneRepository _workZoneRepository;

        public WorkZoneController(IWorkZoneRepository workZoneRepository)
        {
            _workZoneRepository = workZoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orgs = await _workZoneRepository.Retrieve();
            return Ok(orgs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var org = await _workZoneRepository.Retrieve(id);

            if (org != null)
            {
                return Ok(org);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(WorkZoneDto workZoneDto)
        {
            var result = await _workZoneRepository.Insert(workZoneDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(WorkZoneDto workZoneDto)
        {
            var result = await _workZoneRepository.Update(workZoneDto);


            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _workZoneRepository.Delete(id);
            return Ok(result);
        }
    }
}
