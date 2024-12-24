using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> Retrieve()
        {
            var menus = await _menuRepository.Retrieve();
            return Ok(menus);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Retrieve(Guid id)
        {
            var menu = await _menuRepository.Retrieve(id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpPost]
        public async Task<ActionResult> InsertMenu([FromBody] MenuDto menuDto)
        {
            if (menuDto == null)
            {
                return NoContent();
            }

            var success = await _menuRepository.Insert(menuDto);
            if(success)
            {
                return Ok(menuDto);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMenu(Guid id, [FromBody] MenuDto menuDto)
        {
            if (menuDto == null || menuDto.Id != id)
            {
                return BadRequest("Invalid menu data.");
            }

            var success = await _menuRepository.Update(menuDto);
            if(success)
            {
                return Ok(menuDto);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(Guid id)
        {
            var success = await _menuRepository.Delete(id);
            if (success)
            {
                return Ok(success);
            }
            if (!success)
            {
                return NotFound(success);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin, Superadmin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteMenu([FromBody] MenuDto menuDto)
        {
            if (menuDto == null)
            {
                return BadRequest("Menu data is required.");
            }

            var success = await _menuRepository.Delete(menuDto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
