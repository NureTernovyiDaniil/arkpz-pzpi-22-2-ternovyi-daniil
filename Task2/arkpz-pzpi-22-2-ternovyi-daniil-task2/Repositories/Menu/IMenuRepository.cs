using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> Retrieve();
        Task<Menu> Retrieve(Guid id);
        Task<bool> Insert(MenuDto menuItem);
        Task<bool> Update(MenuDto menuItem);
        Task<bool> Delete(MenuDto menuItem);
        Task<bool> Delete(Guid id);
    }
}
