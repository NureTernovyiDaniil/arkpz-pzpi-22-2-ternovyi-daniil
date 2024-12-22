using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ChefMate_backend.Enums;

namespace ChefMate_backend.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string? WaiterId { get; set; }
        public DateTime OrderDate { get; set; }
        public string TableNum { get; set; }
        public decimal? TotalAmount { get; set; }
        public int TotalTimeForCooking { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
