using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChefMate_backend.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        public Guid MenuItemId { get; set; }
        [JsonIgnore]
        public virtual MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
