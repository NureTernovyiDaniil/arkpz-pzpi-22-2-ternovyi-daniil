using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Menu>> Retrieve()
        {
            return await _context.Menus.Include(x=>x.Items).ToListAsync();
        }

        public async Task<Menu?> Retrieve(Guid id)
        {
            var entity = await _context.Menus.Include(x => x.Items).FirstOrDefaultAsync(m => m.Id == id);
            return entity;
        }

        public async Task<bool> Update(MenuDto menuItem)
        {
            var entity = await _context.Menus.FindAsync(menuItem.Id);
            if (entity == null)
            {
                return false;
            }

            _mapper.Map(menuItem, entity);
            _context.Menus.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
