namespace e_commerce_Api.Models
{
    public class TokenHistory
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}