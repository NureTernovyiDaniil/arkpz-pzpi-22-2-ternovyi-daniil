using ChefMate_backend.Enums;

namespace ChefMate_backend.Models
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Priority { get; set; }
        public KitchenTaskStatus Status { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
