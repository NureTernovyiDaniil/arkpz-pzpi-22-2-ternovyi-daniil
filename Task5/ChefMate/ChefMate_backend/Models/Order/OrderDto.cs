using ChefMate_backend.Enums;
using System.Text.Json.Serialization;

namespace ChefMate_backend.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string WaiterId { get; set; }
        public string ChefId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
