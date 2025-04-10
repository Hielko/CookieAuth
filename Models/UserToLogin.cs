namespace CookieAuth.Models
{
    public class UserToLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? AccountType { get; set; }

    }
}