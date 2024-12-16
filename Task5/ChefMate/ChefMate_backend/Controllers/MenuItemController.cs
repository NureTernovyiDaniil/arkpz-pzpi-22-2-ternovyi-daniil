using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var menuItems = await _menuItemRepository.Retrieve();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var menuItem = await _menuItemRepository.Retrieve(id);
            return Ok(menuItem);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("post")]
        public async Task<IActionResult> Post(MenuItemDto menuItem)
        {
            var result = await _menuItemRepository.Insert(menuItem);
            if(result)
            {
                return Ok(menuItem);
            }

            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(MenuItemDto menuItem)
        {
            var result = await _menuItemRepository.Update(menuItem);
            if (result)
            {
                return Ok(menuItem);
            }

            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _menuItemRepository.Delete(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(MenuItemDto menuItem)
        {
            var result = await _menuItemRepository.Delete(menuItem);
            if (result)
            {
                return Ok(menuItem);
            }

            return BadRequest();
        }
    }
}
