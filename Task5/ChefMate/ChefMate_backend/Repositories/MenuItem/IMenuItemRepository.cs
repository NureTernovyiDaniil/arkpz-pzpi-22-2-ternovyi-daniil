using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> Retrieve();
        Task<MenuItem> Retrieve(Guid id);
        Task<bool> Insert(MenuItemDto menuItem);
        Task<bool> Update(MenuItemDto menuItem);
        Task<bool> Delete(MenuItemDto menuItem);
        Task<bool> Delete(Guid id);
    }
}