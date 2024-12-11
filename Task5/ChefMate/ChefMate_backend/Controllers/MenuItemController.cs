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

        [HttpPost("post")]
        public async Task<IActionResult> Post(MenuItemDto menuItem)
        {
            await _menuItemRepository.Insert(menuItem);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(MenuItemDto menuItem)
        {
            await _menuItemRepository.Update(menuItem);
            return Ok();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var menuItem = await _menuItemRepository.Retrieve(id);
            await _menuItemRepository.Delete(menuItem);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(MenuItemDto menuItem)
        {
            await _menuItemRepository.Delete(menuItem);
            return Ok();
        }
    }
}
