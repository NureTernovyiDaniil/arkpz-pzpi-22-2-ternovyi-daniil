using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> Retrieve();
        Task<OrderItem> Retrieve(Guid orderItemId);
        Task<bool> Insert(OrderItemDto orderItem);
        Task<bool> Insert(List<OrderItemDto> orderItem);
        Task<bool> Update(OrderItemDto orderItem);
        Task<bool> Delete(Guid orderItemId);
        Task<bool> Delete(OrderItemDto orderItem);
    }
}