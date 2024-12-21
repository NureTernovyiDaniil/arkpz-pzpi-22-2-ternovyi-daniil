using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefMate_backend.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
