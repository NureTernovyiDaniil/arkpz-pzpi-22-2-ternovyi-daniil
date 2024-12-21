using System.Text.Json.Serialization;

namespace ChefMate_backend.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

        public string Feedback { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
