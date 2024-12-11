using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MenuItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> Retrieve()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto> Retrieve(Guid id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
                throw new KeyNotFoundException("MenuItem not found.");
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<bool> Insert(MenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            await _context.MenuItems.AddAsync(menuItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(MenuItemDto menuItemDto)
        {
            var existingMenuItem = await _context.MenuItems.FindAsync(menuItemDto.Id);
            if (existingMenuItem == null)
            {
                throw new KeyNotFoundException("MenuItem not found.");
            }

            _mapper.Map(menuItemDto, existingMenuItem);
            _context.MenuItems.Update(existingMenuItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(MenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            _context.MenuItems.Remove(menuItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                throw new KeyNotFoundException("MenuItem not found.");
            }

            _context.MenuItems.Remove(menuItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
