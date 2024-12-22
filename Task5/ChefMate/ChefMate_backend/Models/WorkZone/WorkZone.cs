using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChefMate_backend.Models
{
    public class WorkZone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
