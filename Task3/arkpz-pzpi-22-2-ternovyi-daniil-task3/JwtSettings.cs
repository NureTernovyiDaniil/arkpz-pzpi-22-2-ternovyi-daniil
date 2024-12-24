namespace ChefMate_backend
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifeTimeMinutes { get; set; }
    }
}
