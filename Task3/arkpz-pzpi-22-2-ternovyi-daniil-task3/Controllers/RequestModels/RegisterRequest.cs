namespace ChefMate_backend.Controllers.RequestModels
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
