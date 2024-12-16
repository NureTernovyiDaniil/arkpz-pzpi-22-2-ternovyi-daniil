namespace ChefMate_backend.Models
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
