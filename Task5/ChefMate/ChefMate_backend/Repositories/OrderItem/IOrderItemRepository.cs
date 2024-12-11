using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItemDto>> Retrieve();
        Task<OrderItemDto> Retrieve(Guid orderItemId);
        Task<bool> Insert(OrderItemDto orderItem);
        Task<bool> Update(OrderItemDto orderItem);
        Task<bool> Delete(Guid orderItemId);
        Task<bool> Delete(OrderItemDto orderItem);
    }
}