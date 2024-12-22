using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ChefMate_backend.Enums;

namespace ChefMate_backend.Models
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public int TimeForCooking { get; set; }
        public MenuItemCategory Category { get; set; }
        public Guid WorkZoneId { get; set; }
        public WorkZone WorkZone { get; set; }
        public Guid MenuId { get; set; }
        [JsonIgnore]
        public virtual Menu Menu { get; set; }
    }
}
