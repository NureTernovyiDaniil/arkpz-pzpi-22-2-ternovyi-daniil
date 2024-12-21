namespace ChefMate_backend.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string? WaiterId { get; set; }
        public DateTime OrderDate { get; set; }
        public string TableNum { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
