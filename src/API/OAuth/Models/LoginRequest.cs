namespace API.OAuth.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
    }
}
