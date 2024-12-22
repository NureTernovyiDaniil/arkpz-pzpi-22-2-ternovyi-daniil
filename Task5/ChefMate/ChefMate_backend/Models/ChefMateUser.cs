using Microsoft.AspNetCore.Identity;

namespace ChefMate_backend.Models
{
    public class ChefMateUser : IdentityUser<Guid>
    {
        public Guid? OrganizationId { get; set; }
    }
}
