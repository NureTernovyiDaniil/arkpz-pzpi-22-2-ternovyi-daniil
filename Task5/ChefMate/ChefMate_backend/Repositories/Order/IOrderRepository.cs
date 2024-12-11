using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> Retrieve();
        Task<OrderDto> Retrieve(Guid orderId);
        Task<bool> Insert(OrderDto order);
        Task<bool> Update(OrderDto order);
        Task<bool> Delete(Guid orderId);
        Task<bool> Delete(OrderDto order);
    }
}
