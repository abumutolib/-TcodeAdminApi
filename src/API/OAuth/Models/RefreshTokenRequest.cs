namespace API.OAuth.Models
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string ClientId { get; set; }
    }
}
