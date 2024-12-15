using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefMate_backend.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MenuRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(MenuDto menuItem)
        {
            var entity = _mapper.Map<Menu>(menuItem);
            _context.Menus.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Menus.FindAsync(id);
            if (entity == null)
                return false;

            _context.Menus.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Insert(MenuDto menuItem)
        {
            var entity = _mapper.Map<Menu>(menuItem);
            await _context.Menus.AddAsync(entity);
            menuItem.Id = entity.Id;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MenuDto>> Retrieve()
        {
            var entities = await _context.Menus.ToListAsync();
            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }

        public async Task<MenuDto> Retrieve(Guid id)
        {
            var entity = await _context.Menus.FindAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<MenuDto>(entity);
        }

        public async Task<bool> Update(MenuDto menuItem)
        {
            var entity = await _context.Menus.FindAsync(menuItem.Id);
            if (entity == null)
                return false;

            _mapper.Map(menuItem, entity);
            _context.Menus.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
