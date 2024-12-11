using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItemDto>> Retrieve();
        Task<MenuItemDto> Retrieve(Guid id);
        Task<bool> Insert(MenuItemDto menuItem);
        Task<bool> Update(MenuItemDto menuItem);
        Task<bool> Delete(MenuItemDto menuItem);
        Task<bool> Delete(Guid id);
    }
}